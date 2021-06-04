﻿using blazorTest.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace blazorTest.Client.Models
{
    public class PostModel
    {

        public List<ThreadModel> ThreadModels { get; set; }

        public string UserEmail { get; set; }

        public string HandleName { get; set; }

        public Guid PostId { get; set; }

        public string MessageContext { get; set; }

        public DateTime CreateDate { get; set; }


        public async Task PostThreadMessage(ThreadMessage message, HttpClient httpClient)
        {
            var response = await httpClient.PostAsJsonAsync("Post", message);

            var threadMessage = await response.Content.ReadFromJsonAsync<ThreadMessage>();

            ThreadModels.Add(new ThreadModel()
            {
                UserEmail = threadMessage.UserEmail,
                HandleName = threadMessage.HandleName,
                ThreadId = threadMessage.ThreadId,
                MessageContext = threadMessage.MessageContext,
                CreateDate = threadMessage.CreateDate
            });
        }


    }
}
