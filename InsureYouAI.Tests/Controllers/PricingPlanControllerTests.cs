using InsureYouAI.Context;
using InsureYouAI.Controllers;
using InsureYouAI.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Xunit;

public class PricingPlanControllerTests
{
    private InsureContext GetInMemoryContext()
    {
        var options = new DbContextOptionsBuilder<InsureContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        return new InsureContext(options);
    }

    [Fact]
    public void ChangeStatus_Should_Toggle_IsFeature()
    {
        // Arrange
        var context = GetInMemoryContext();
        var plan = new PricingPlan
        {
            PricingPlanId = 1,
            IsFeature = false,
            Title = "Test Planı"
        };

        context.PricingPlans.Add(plan);
        context.SaveChanges();

        var controller = new PricingPlanController(context);

        // --- KRİTİK EKLEME: User nesnesini simüle ediyoruz ---
        var user = new System.Security.Claims.ClaimsPrincipal(new System.Security.Claims.ClaimsIdentity());
        controller.ControllerContext = new Microsoft.AspNetCore.Mvc.ControllerContext
        {
            HttpContext = new Microsoft.AspNetCore.Http.DefaultHttpContext { User = user }
        };
        // -----------------------------------------------------

        // Act
        var result = controller.ChangeStatus(1);

        // Assert
        var updatedPlan = context.PricingPlans.Find(1);
        Assert.NotNull(updatedPlan);
        Assert.True(updatedPlan.IsFeature);
        Assert.IsType<RedirectToActionResult>(result);
    }

    [Fact]
    public void DeletePricingPlan_Should_Remove_Plan_From_Database()
    {
        // Arrange
        var context = GetInMemoryContext();
        var plan = new PricingPlan { PricingPlanId = 99, Title = "Silinecek Plan" };
        context.PricingPlans.Add(plan);
        context.SaveChanges();

        var controller = new PricingPlanController(context);

        // Loglama hatası almamak için User'ı yine mock'lıyoruz
        var user = new System.Security.Claims.ClaimsPrincipal(new System.Security.Claims.ClaimsIdentity());
        controller.ControllerContext = new Microsoft.AspNetCore.Mvc.ControllerContext
        {
            HttpContext = new Microsoft.AspNetCore.Http.DefaultHttpContext { User = user }
        };

        // Act
        var result = controller.DeletePricingPlan(99);

        // Assert
        var deletedPlan = context.PricingPlans.Find(99);
        Assert.Null(deletedPlan); // Veritabanında artık olmamalı
        Assert.IsType<RedirectToActionResult>(result); // Listeye yönlendirmeli
    }

    [Fact]
    public void DeletePricingPlan_WithInvalidId_ShouldReturnRedirect()
    {
        // Arrange
        var context = GetInMemoryContext(); // Boş bir DB
        var controller = new PricingPlanController(context);

        // Sahte kullanıcı
        var user = new System.Security.Claims.ClaimsPrincipal(new System.Security.Claims.ClaimsIdentity());
        controller.ControllerContext = new Microsoft.AspNetCore.Mvc.ControllerContext
        {
            HttpContext = new Microsoft.AspNetCore.Http.DefaultHttpContext { User = user }
        };

        // Act
        var result = controller.DeletePricingPlan(999); // DB'de 999 yok

        // Assert
        var redirectResult = Assert.IsType<RedirectToActionResult>(result);
        Assert.Equal("PricingPlanList", redirectResult.ActionName);
    }

}
