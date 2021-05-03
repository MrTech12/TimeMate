using System;
using System.Collections.Generic;
using System.Text;
using TimeMate.Models;

namespace TimeMate.Services
{
    public class AppointmentService
    {
        public bool ValidateDates(AppointmentViewModel viewModel)
        {
            if (viewModel.EndDate < viewModel.StartDate)
            {
                return false;
            }
            else if (viewModel.StartDate > viewModel.EndDate)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
