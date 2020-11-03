﻿using Application.ViewModel.Common;

namespace Application.ViewModel.Password
{
    public class PasswordAddDto : EntityAddDto, IPasswordProperty
    {
        public string Username { get; set; }
        public string EmailAddress { get; set; }
        public string Password { get; set; }
        public string UsedIn { get; set; }
    }
}
