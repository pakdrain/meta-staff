using Google.Protobuf;
using HrmApi.DAL;
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

    public class MachineAttendanceReturn
    {
        public int Status { get; set; }
        public string Message { get; set; }
        public MachineAttendance MachineAttData { get; set; }
        public List<MachineAttendance> MachineAttDataList { get; set; }
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

    public class LoanSummaryReturn
    {
        public int Status { get; set; }
        public string Message { get; set; }
        public LoanSummary LoanSummaryData { get; set; }
        public List<LoanSummary> LoanSummaryDataList { get; set; }
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

    public class LoansReturn
    {
        public int Status { get; set; }
        public string Message { get; set; }
        public LoansModel LoanData { get; set; }
        public List<LoansModel> LoanDataList { get; set; }
    }

    public class BonusDeductionReturn
    {
        public int Status { get; set; }

        public string Message { get; set; }

        public int new_id { get; set; }

        public List<BonusDeduction> BonusDeductionData { get; set; }

        public List<DeductionMaster> DeductionMaster { get; set; }
    }

    public class WpPolicyReturn
    {
        public int Status { get; set; }
        public string Message { get; set; }
        public int new_wp_id { get; set; }
        public WpPolicyModel WpPolicyData { get; set; }
        public List<WpPolicyModel> WpPolicyDataList { get; set; }
    }

    public class ProjectReturn
    {
        public int Status { get; set; }
        public string Message { get; set; }
        public int new_proj_id { get; set; }
        public ProjectModel ProjectData { get; set; }
        public List<ProjectModel> ProjectsDataList { get; set; }
    }


    public class ProgTrackReturn
    {
        public int Status { get; set; }
        public string Message { get; set; }
        public int new_proj_id { get; set; }
        public ProgTrack ProgTrackData { get; set; }
        public List<ProgTrack> ProgTrackDataList { get; set; }
    }

    public class HcReturn
    {
        public int Status { get; set; }
        public string Message { get; set; }
        public int new_hc_id { get; set; }
        public HcModel HcData { get; set; }
        public List<HcModel> HcDataList { get; set; }
    }

    public class SalSlipReturn
    {
        public int Status { get; set; }
        public string Message { get; set; }
        public int new_slip_id { get; set; }
        public SalSlipModel SalSlipData { get; set; }
        public List<SalSlipModel> SalSlipDataList { get; set; }
    }

    public class EvalFormReturn
    {
        public int Status { get; set; }
        public string Message { get; set; }
        public int new_slip_id { get; set; }
        public EvalFormModel EvalFormData { get; set; }
        public List<EvalFormModel> EvalFormDataList { get; set; }
    }

    public class ApprEmpRequest
    {
        public int Operation { get; set; }
        public string app_user { get; set; }
        public List<ApprEmpModel> items { get; set; }
    }

    public class ApprEmpReturn
    {
        public int Status { get; set; }
        public string Message { get; set; }
        public int Operation { get; set; }
        public string user_type { get; set; }
        public string bell_curve_applied { get; set; }
        public ApprEmpModel ApprEmpData { get; set; }
        public List<ApprEmpModel> ApprEmpDataList { get; set; }
    }

    public class ApprSummariesReturn
    {
        public int Status { get; set; }
        public string Message { get; set; }
        
        public ApprSummariesModel ApprSummariesData { get; set; }
        public List<ApprSummariesModel> ApprSummariesDataList { get; set; }
    }

    public class ApprSetupReturn
    {
        public int Status { get; set; }
        public string Message { get; set; }
      
        public ApprSetupModel ApprSetupData { get; set; }
        public List<ApprSetupModel> ApprSetupDataList { get; set; }
    }

    public class RankingReturn
    {
        public int Status { get; set; }
        public string Message { get; set; }
        public RankingModel RankingData { get; set; }
        public List<RankingModel> RankingDataList { get; set; }
    }

    public class SheetReturn
    {
        public int Status { get; set; }
        public string Message { get; set; }
        public SheetModel SheetData { get; set; }
        public List<SheetModel> SheetDataList { get; set; }
    }

    public class SheetDetReturn
    {
        public int Status { get; set; }
        public string Message { get; set; }
        public SheetDetModel SheetDetData { get; set; }
        public List<SheetDetModel> SheetDetDataList { get; set; }
    }

    public class OwnedVehicleReturn
    {
        public int Status { get; set; }
        public string Message { get; set; }
        public OwnedVehicle OwnedVehicleData { get; set; }
        public List<OwnedVehicle> OwnedVehicleDataList { get; set; }
    }

    public class VehiclesReturn
    {
        public int Status { get; set; }
        public string Message { get; set; }
        public int new_vehicle_id { get; set; }
        public VehiclesModel VehiclesData { get; set; }
        public List<VehiclesModel> VehiclesDataList { get; set; }
    }

    public class VehiclesDetReturn
    {
        public int Status { get; set; }
        public string Message { get; set; }
        public VehiclesDet VehiclesDetData { get; set; }
        public List<VehiclesDet> VehiclesDetDataList { get; set; }
    }

    public class TaskTypesReturn
    {
        public int Status { get; set; }
        public string Message { get; set; }
        public TaskTypes TaskTypesData { get; set; }
        public List<TaskTypes> TaskTypesDataList { get; set; }
    }

    public class TasksModelReturn
    {
        public int Status { get; set; }
        public string Message { get; set; }
        public TasksModel TaskTypesData { get; set; }
        public List<TasksModel> TasksDataList { get; set; }
    }


    public class ComplainReturn
    {
        public int Status { get; set; }
        public string Message { get; set; }
        public int new_cmplnt_id { get; set; }

        public ComplainModel ComplainData { get; set; }
        public List<ComplainModel> ComplainDataList { get; set; }
    }

    public class NotifReturn
    {
        public int Status { get; set; }
        public string Message { get; set; }
        public NotifModel NotifData { get; set; }
        public List<NotifModel> NotifDataList { get; set; }
    }

    public class LoanSchedualeReturn
    {
        public int Status { get; set; }
        public string Message { get; set; }
        public LoanSchedualeModel LoanSchedualeData { get; set; }

        public List<LoanSchedualeModel> LoanSchedualeDataList { get; set; }
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

 

    public class LoanReturn
    {
        public int Status { get; set; }
        public string Message { get; set; }
        public int avail_limit { get; set; }
        public int prev_loan { get; set; }
        public int no_of_loan { get; set; }
        public int balance { get; set; }
       

        public LoansModel LoanData { get; set; }
        public List<LoansModel> LoanDataList { get; set; }
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

