using SkiaSharp;
using SkiaSharp.Views.Maui;
using SkiaSharp.Views.Maui.Controls;
using System;
using System.Linq;
using System.Collections.Generic;

namespace PersonalFinanceTracker.Pages;

public partial class ReportsPage : ContentPage
{
    private decimal totalIncome = 0;
    private decimal totalExpense = 0;

    public ReportsPage()
	{
		InitializeComponent();
        LoadTransactionData();

    }

    private async void LoadTransactionData()
    {
        var transactions = await App.TransactionDB.GetTransactions();

        totalIncome = transactions.Where(t => t.Amount > 0).Sum(t => t.Amount);
        totalExpense = transactions.Where(t => t.Amount < 0).Sum(t => Math.Abs(t.Amount));

        // Refresh the chart after loading data
        this.FindByName<SKCanvasView>("skiaCanvas").InvalidateSurface();
    }

    private void OnCanvasViewPaintSurface(object sender, SKPaintSurfaceEventArgs args)
    {
        var surface = args.Surface;
        var canvas = surface.Canvas;
        var info = args.Info;

        canvas.Clear();

        if (totalIncome == 0 && totalExpense == 0)
        {
            return; // No data, don't draw anything
        }

        // Define chart colors
        SKColor incomeColor = SKColors.Green;
        SKColor expenseColor = SKColors.Red;

        // Calculate total
        decimal totalAmount = totalIncome + totalExpense;
        float incomeAngle = (float)(360 * (totalIncome / totalAmount));
        float expenseAngle = (float)(360 * (totalExpense / totalAmount));

        // Create paint objects
        using (var paint = new SKPaint { Style = SKPaintStyle.Fill, IsAntialias = true })
        {
            var center = new SKPoint(info.Width / 2, info.Height / 2);
            float radius = Math.Min(info.Width, info.Height) / 3;

            // Draw Income section
            paint.Color = incomeColor;
            using (var path = new SKPath())
            {
                path.MoveTo(center);
                path.ArcTo(new SKRect(center.X - radius, center.Y - radius, center.X + radius, center.Y + radius),
                           0, incomeAngle, false);
                path.Close();
                canvas.DrawPath(path, paint);
            }

            // Draw Expense section
            paint.Color = expenseColor;
            using (var path = new SKPath())
            {
                path.MoveTo(center);
                path.ArcTo(new SKRect(center.X - radius, center.Y - radius, center.X + radius, center.Y + radius),
                           incomeAngle, expenseAngle, false);
                path.Close();
                canvas.DrawPath(path, paint);
            }
        }
    }

    private async void OnBackButtonClicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }
}