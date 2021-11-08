using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using DrikusApp.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;

namespace DrikusApp.Pages
{
    public partial class Index
    {
        private HubConnection _connection;
        private MessageForm messageForm = new();
        public List<MessageModel> Messages { get; set; } = new();

        protected override async Task OnInitializedAsync()
        {
            _connection = new HubConnectionBuilder()
                .WithUrl("https://chattyapi.azurewebsites.net/ChatHub")
                .WithAutomaticReconnect()
                .Build();

            _connection.On<string, string>(
                "ReceiveMessage",
                (user, message) =>
                {
                    var url = GetUrlFromString(message);
                    var messageModel = new MessageModel
                    {
                        Name = user,
                        Message = new MarkupString(message),
                        Url = url
                    };
                    Console.Write($"\r{user}: {message}\n>");
                    Messages.Add(messageModel);
                    StateHasChanged();
                });

            await _connection.StartAsync();
        }

        private async Task HandleValidSubmitAsync()
        {
            // Process the valid form

            // Send to socket
            await _connection.InvokeAsync("SendMessage", messageForm.Name, messageForm.Message);

            // Reset the form
            messageForm.Message = "";
        }

        private string GetUrlFromString(string message)
        {
            string pattern = @"^(?:http(s)?:\/\/)?[\w.-]+(?:\.[\w\.-]+)+[\w\-\._~:/?#[\]@!\$&'\(\)\*\+,;=.]+$";
            Regex filter = new Regex(pattern);

            var match = filter.Match(message);
            if (match.Success)
            {
                return match.Value;
            }
            else
            {
                return string.Empty;
            }
        }
    }
}