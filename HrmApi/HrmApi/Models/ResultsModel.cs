using Google.Protobuf;
using HrmApi.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;

namespace HrmApi.Models
{
    public class Result
    {
        public string Status { get; set; }
        public string Message { get; set; }
    }

    public class CompanyReturn
    {
        public int Status { get; set; }
        public string Message { get; set; }
        public CompaniesModel CompaniesData { get; set; }
        public List<CompaniesModel> CompaniesDataList { get; set; }
    }

    public class RoleReturn
    {
        public int Status { get; set; }
        public string Message { get; set; }
        public RoleModel RoleData { get; set; }
        public List<RoleModel> RoleDataList { get; set; }
    }

    public class ExtensionReturn
    {
        public int Status { get; set; }
        public string Message { get; set; }
        public ExtensionModel ExtensionData { get; set; }
        public List<ExtensionModel> ExtensionDataList { get; set; }
    }

    public class EmpTrackingReturn
    {
        public int Status { get; set; }
        public string Message { get; set; }
        public EmployeeTrackingModel EmpTrackingData { get; set; }
        public List<EmployeeTrackingModel> EmpTrackingDataList { get; set; }
    }

    public class AttendanceReturn
    {
        public int Status { get; set; }
        public string Message { get; set; }
        public string today_check_in_time { get; set; }
        public string today_check_out_time { get; set; }
        public int present { get; set; }
        public int absent { get; set; }
        public int late_in { get; set; }
        public int early_out { get; set; }
        public int awaiting { get; set; }

        public AttendanceModel AttendanceData { get; set; }
        public List<AttendanceModel> AttendanceDataList { get; set; }
    }

    public class DepartmentReturn
    {
        public int Status { get; set; }
        public string Message { get; set; }
        public DepartmentModel DepartmentData { get; set; }
        public List<DepartmentModel> DepartmentDataList { get; set; }
    }

    public class OutDoorReturn
    {
        public int Status { get; set; }
        public string Message { get; set; }
        public OutDoorModel OutDoorData { get; set; }
        public List<OutDoorModel> OutDoorDataList { get; set; }
    }

    public class TestReturn
    {
        public int Status { get; set; }
        public string Message { get; set; }
        public TestModel TestData { get; set; }
        public List<TestModel> TestDataList { get; set; }
    }

    public class EmployeeReturn
    {
        public int Status { get; set; }
        public string Message { get; set; }
        public EmployeeModel EmployeeData { get; set; }
        public List<EmployeeModel> EmployeeDataList { get; set; }
    }

    public class ClientReturn
    {
        public int Status { get; set; }
        public string Message { get; set; }
        public ClientModel ClientData { get; set; }
        //public List<EmployeeModel> EmployeeDataList { get; set; }
    }

    public class ClearanceReturn
    {
        public int Status { get; set; }
        public string Message { get; set; }
        public ClearanceModel ClearanceData { get; set; }

        public List<ClearanceModel> ClearanceDataList { get; set; }
    }

    public class OfficeReturn
    {
        public int Status { get; set; }
        public string Message { get; set; }
        public OfficeModel OfficeData { get; set; }

        public List<OfficeModel> OfficeDataList { get; set; }
    }

    public class RegionReturn
    {
        public int Status { get; set; }
        public string Message { get; set; }
        public RegionModel RegionData { get; set; }

        public List<RegionModel> RegionDataList { get; set; }
    }

    public class LeaveReturn
    {
        public int Status { get; set; }
        public string Message { get; set; }
        public decimal no_of_leave_days { get; set; }

        public decimal total_sick_leave { get; set; }
        public decimal sick_leave_bal { get; set; }

        public decimal total_anual_leave { get; set; }
        public decimal anual_leave_bal { get; set; }

        public decimal total_casual_leave { get; set; }
        public decimal casual_leave_bal { get; set; }

        public LeaveModel   LeaveData { get; set; }
        public List<LeaveModel> LeaveDataList { get; set; }
    }
    public class ComplainReturn
    {
        public int Status { get; set; }
        public string Message { get; set; }
        public ComplainModel LeaveData { get; set; }
        public List<ComplainModel> LeaveDataList { get; set; }
    }

    public class ItemReturn
    {
        public int Status { get; set; }
        public string Message { get; set; }
        public ItemModel ItemsData { get; set; }
        public List<ItemModel> ItemsDataList { get; set; }
    }

    public class ResignReturn
    {
        public int Status { get; set; }
        public string Message { get; set; }
        public ResignModel ResignData { get; set; }
        public List<ResignModel> ResignDataList { get; set; }
    }

    public class LoanTypeReturn
    {
        public int Status { get; set; }
        public string Message { get; set; }
        public LoanTypeModel LoanTypeData { get; set; }
        public List<LoanTypeModel> LoanTypeDataList { get; set; }
    }

    public class LoanReturn
    {
        public int Status { get; set; }
        public string Message { get; set; }
        public int avail_limit { get; set; }
        public int prev_loan { get; set; }
        public int no_of_loan { get; set; }
        public int balance { get; set; }
       

        public LoanModel LoanData { get; set; }
        public List<LoanModel> LoanDataList { get; set; }
    }

    public class LoanDetReturn
    {
        public int Status { get; set; }
        public string Message { get; set; }
        public LoanDetailModel loanDetData { get; set; }
        public List<LoanDetailModel> loanDetDataList { get; set; }
    }

    public class NotificationReturn
    {
        public int Status { get; set; }
        public string Message { get; set; }
        public NotificationModel NotificationData { get; set; }
        public List<NotificationModel> NotificationDataList { get; set; }
    }

    public class VehicleReturn
    {
        public int Status { get; set; }
        public string Message { get; set; }
        public VehicleModel VehicleData { get; set; }
        public List<VehicleModel> VehicleDataList { get; set; }
    }

    public class DashHourReturn
    {
        public int Status { get; set; }
        public string Message { get; set; }
        public DashWorkHoursModel DashHourData { get; set; }
        public List<DashWorkHoursModel> DashHourDataList { get; set; }
    }

    public class DashAttReturn
    {
        public int Status { get; set; }
        public string Message { get; set; }
        public DashboardAttendance DashAttData { get; set; }
    }

    public class DashLeaveBalReturn
    {
        public int Status { get; set; }
        public string Message { get; set; }
        public DashLeaveBalModel DashLeaveBalData { get; set; }
    }
    public class TaskChatsReturn
    {
        public int Status { get; set; }
        public string Message { get; set; }
        public TaskChat TaskChatData { get; set; }
        public List<TaskChat> TaskChatDataList { get; set; }

    }

    public class TaskTypeAndListsReturn
    {
        public int Status { get; set; }
        public string Message { get; set; }
        public TaskTypeAndLists TaskTypeAndListsData { get; set; }
        public List<TaskTypeAndLists> TaskTypeAndListsDataList { get; set; }

    }

    public class TaskReturn
    {
        public int Status { get; set; }
        public string Message { get; set; }
        public int total_task { get; set; }
        public int in_prog_task { get; set; }
        public int complete_task { get; set; }

        public int gave_total_task { get; set; }
        public int gave_in_prog_task { get; set; }
        public int gave_complete_task { get; set; }

        public int poc_total_task { get; set; }
        public int poc_in_prog_task { get; set; }
        public int poc_complete_task { get; set; }

        public TaskModel TaskData { get; set; }
        public List<TaskModel> TaskDataList { get; set; }
    }


}

public class OrderMastReturn
{
    public int Status { get; set; }
    public string Message { get; set; }

    public OrderMastModel OrderData { get; set; }
    public List<OrderMastModel> OrderDataList { get; set; }
}

public class OrderDetReturn
{
    public int Status { get; set; }
    public string Message { get; set; }

    public OrderDetModel OrderDetData { get; set; }
    public List<OrderDetModel> OrderDetDataList { get; set; }
}

