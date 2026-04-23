using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Wedding_Planner.Models
{
    public class DashboardVM
    {
        public int TotalBookings { get; set; }
        public int TotalFeedbacks { get; set; }
        public int TotalComplaints { get; set; }
        public int TotalEmails { get; set; }

    }
}