using InsureYouAI.Context;
using InsureYouAI.Controllers;
using InsureYouAI.Entities;
using InsureYouAI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsureYouAI.Tests.Controllers
{
    public class MessageControllerTests
    {
        private InsureContext GetInMemoryContext()
        {
            var options = new DbContextOptionsBuilder<InsureContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            return new InsureContext(options);
        }


        [Fact]
        public async Task CreateMessage_Should_Save_With_AI_Tags()
        { 
            var mockAIService=new Mock<IAIService>();

            mockAIService.Setup(x => x.PredictCategoryAsync(It.IsAny<string>()))
                .ReturnsAsync("Kasko");
            mockAIService.Setup(x => x.PredictPriorityAsync(It.IsAny<string>()))
                .ReturnsAsync("High");

            var context = GetInMemoryContext();

            var controller=new MessageController(context, mockAIService.Object);

            var newMessage = new Message { Subject = "Kaza", MessageDetail = "Aracım hasar aldı", Email = "test@test.com", NameSurname = "Test Kullanıcı"
            };

            var result=await controller.CreateMessage(newMessage);

            Assert.Single(context.Messages);

            var savedMessage = context.Messages.First();
            Assert.Equal("Kasko",savedMessage.AICategory);
            Assert.Equal("High",savedMessage.Priority);
            Assert.IsType<RedirectToActionResult>(result);

        }

        [Fact]
        public void UpdateMessage_Should_Update_Existing_Record()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<InsureContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var context = new InsureContext(options);

            var existingMessage = new Message { MessageId = 1, Subject = "Eski", Email = "t@t.com", NameSurname = "X", IsRead = false ,
                MessageDetail = "Bu alan boş kalmamalı"};
                context.Messages.Add(existingMessage);
            context.SaveChanges();

            var mockAI = new Mock<IAIService>();
            var controller = new MessageController(context, mockAI.Object);

            // Act
            existingMessage.IsRead = true; // Durumu değiştirdik
            controller.UpdateMessage(existingMessage);

            // Assert
            var updated = context.Messages.Find(1);
            Assert.True(updated.IsRead);
        }

        [Fact]
        public void DeleteMessage_Should_Remove_Record()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<InsureContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var context = new InsureContext(options);

            var msg = new Message { MessageId = 5, Subject = "Silinecek", Email = "t@t.com", NameSurname = "X",
                MessageDetail = "Bu alan boş kalmamalı" };
                context.Messages.Add(msg);
            context.SaveChanges();

            var mockAI = new Mock<IAIService>();
            var controller = new MessageController(context, mockAI.Object);

            // Act
            controller.DeleteMessage(5);

            // Assert
            var deleted = context.Messages.Find(5);
            Assert.Null(deleted); // Artık null dönmeli
        }
    }
}
