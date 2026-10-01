using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HrmApi.Models
{
    public class MachineAttendance
    {
        public int Operation { get; set; }
        public int Id { get; set; }                 // Auto-increment primary key
        public string EmpCode { get; set; }         // Employee code
        public DateTime? AttendanceTime { get; set; } // Punch in/out time
        public string DeviceId { get; set; }        // Machine/device ID
        public string Status { get; set; }          // IN/OUT
        public DateTime? CreatedAt { get; set; }     // Record insert timestamp
        public string Remarks { get; set; }         // Optional notes

    }
}