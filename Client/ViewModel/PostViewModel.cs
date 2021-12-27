﻿using System;
using System.Threading.Tasks;
using ChatApp.Client.Models;
using ChatApp.Client.Pages;
using ChatApp.Shared.Models;
using Oniqys.Blazor.ViewModel;
using static ChatApp.Client.Pages.Chat;

namespace ChatApp.Client.ViewModel
{
    public class PostViewModel : ContentBase
    {
        private readonly Chat.IPresenter _presenter;

        public Clickable MessageSender { get; }

        private Message _parentMessage;
        public Message ParentMessage { get => _parentMessage; set => ObjectChangeProcess(ref _parentMessage, value); }

        public string TextAreaId => ParentMessage?.Id.ToString() ?? "messageInput";


        private bool _threadOpened;
        public bool ThreadOpend
        {
            get => _threadOpened;
            set => ValueChangeProcess(ref _threadOpened, value);
        }

        public ContentCollection<Message> Messages { get; } = new();

        private string _inputText;

        public string InputText { get => _inputText; set => ObjectChangeProcess(ref _inputText, value); }

        public PostViewModel(Chat.IPresenter presenter, Func<string, Task> sender, Message parentMessage)
        {
            _presenter = presenter;

            Messages.PropertyChanged += (s, e) => OnPropertyChanged(nameof(Messages));
            Messages.CollectionChanged += (s, e) => OnPropertyChanged(nameof(Messages));

            ParentMessage = parentMessage;

            MessageSender = new Clickable
            {
                Command = new Command(async () =>
                {
                    var text = InputText;
                    InputText = string.Empty;
                    _presenter?.SetFocus(TextAreaId);
                    await sender(text);
                }),
            };
        }
    }
}