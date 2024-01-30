using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.API.Common.Constatnt
{
    public static class ErrorConstant
    {
        public static string UserName_Empty = "UserName_Empty";
        public static string Password_Empty = "Password_Empty";
        public static string Incorrect_UserName_Password = "Incorrect_UserName_Password";
        public static string Email_Empty = "Email_Empty";
        public static string UserName_Exists = "UserName_Exists";
        public static string Activation_Message = "Activation_Message";
        public static string Not_Found = "Not_Found";
        public static string User_Activated = "User_Activated";
        public static string Activation_User_Successfully = "Activation_User_Successfully";
        public static string Email_Not_Exists = "Email_Not_Exists";
        public static string Activation_Code_Expire = "Activation_Code_Expire";
        public static string Password_Changed = "Password_Changed";
        public static string Current_Password_Invalid = "Current_Password_Invalid";
        public static string Mobile_Empty = "Mobile_Empty";
        public static string Mobile_Exists = "Mobile_Exists";
        public static string User_Invalid = "User_Invalid";
        public static string User_Update_Error = "User_Update_Error";
        public static string Email_Credentials_Send_Failure = "Email_Credentials_Send_Failure";
        public static string User_Delete_Error = "User_Delete_Error";

        public static string NotFound_Error = "Record not found.";
        public static string Incorrect_Error = "Incorrect data provided.";
        public static string Failure_Already_Exists_Error = "Record already exists and cannot be added again.";
        public static string Empty_Result_Error = "No records found matching the criteria.";
        public static string Insert_Error = "Failed to insert the record.";
        public static string Update_Error = "Failed to update the record.";
        public static string Delete_Error = "Failed to delete the record.";
        public static string Empty_Update_Error = "No changes were made during the update.";
        public static string InvalidInput_Error = "Invalid input provided.";



    }
}
