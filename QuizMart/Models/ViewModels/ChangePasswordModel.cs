﻿namespace QuizMart.Models.ViewModels
{
    public class ChangePasswordModel
    {
        public string? CurrentPassword { get; set; }

        public string? NewPassword { get; set; }

        public string? ConfirmPassword { get; set; }
    }
}