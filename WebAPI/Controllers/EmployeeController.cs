using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using WebAPI.Models;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _environment;
        public EmployeeController(IConfiguration configuration, IWebHostEnvironment environment)
        {
            _configuration = configuration;
            _environment = environment;
        }

        [HttpGet]
        public JsonResult Get()
        {
            string query = @"Select EmployeeId, EmployeeNo, EmployeeName, Gender, convert(varchar(10),DateOfBirth,120) as DateOfBirth, Email, Mobile, convert(varchar(10),DateofJoining,120) as DateofJoining, Designation, Department, Company, Branch, Remarks from dbo.Employee where Status='A'";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("DBConnection");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult(table);
        }

        [HttpPost]
        public JsonResult Post(Employee Emp)
        {
            string query = @"Insert into dbo.Employee  (EmployeeNo, EmployeeName, Gender, DateOfBirth, Email, Mobile, DateofJoining, Designation, Department, Company, Branch, Remarks, CreatedBy, ModifiedBy) Values (' " + Emp.EmployeeNo + "','" + Emp.EmployeeName + "','" + Emp.Gender + "'," + Emp.DateOfBirth + ",'" + Emp.Email + "','" + Emp.Mobile + "'," + Emp.DateofJoining + ",'" + Emp.Designation + "','" + Emp.Department + "','" + Emp.Company + "','" + Emp.Branch + "','" + Emp.Remarks + "', 'Sandeep', 'Sandeep')";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("DBConnection");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult("Added Successfully");
        }


        [HttpPut]
        public JsonResult Put(Employee Emp)
        {
            string query = @"Update dbo.Employee set  EmployeeNo = '" + Emp.EmployeeNo + "', EmployeeName = '" + Emp.EmployeeName + "', Gender = '" + Emp.Gender + "', DateOfBirth = " + Emp.DateOfBirth + ", Email = '" + Emp.Email + "', Mobile = '" + Emp.Mobile + "', DateofJoining = " + Emp.DateofJoining + ", Designation = '" + Emp.Designation + "', Department = '" + Emp.Department + "', Company = '" + Emp.Company + "', Branch = '" + Emp.Branch + "', Remarks ='" + Emp.Remarks + "',  ModifiedBy='Sandeep', ModifiedOn = GETDATE() where EmployeeId =" + Emp.EmployeeId + "";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("DBConnection");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult("Updated Successfully");
        }


        [HttpDelete("{id}")]
        public JsonResult Delete(int id)
        {
            //string query = @"Delete from dbo.Employee  where EmployeeId =" + id + "";
            string query = @"Update dbo.Employee set Status='I', ModifiedBy='Sandeep', ModifiedOn=GETDATE() where EmployeeId =" + id + "";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("DBConnection");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult("Deleted Successfully");
        }

        [Route("SaveFile")]
        [HttpPost]
        public JsonResult SaveFile()
        {
            try
            {
                var httpRequest = Request.Form;
                var postedFile = httpRequest.Files[0];
                string fileName = postedFile.FileName;
                var physicalPath = _environment.ContentRootPath + "/Photos/" + fileName;
                
                using(var stream = new FileStream(physicalPath, FileMode.Create))
                {
                    postedFile.CopyTo(stream);
                }

                return new JsonResult(fileName);
            }
            catch(Exception)
            {
                return new JsonResult("anonymous.png");
            }
        }

        [Route("api/Employee/GetAllDepartments")]
        [HttpGet]
        public JsonResult GetAllDepartments()
        {
            string query = @"Select DepartmentName from dbo.Department where Status='A'";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("DBConnection");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult(table);
        }

        [Route("api/Employee/GetAllCompanys")]
        [HttpGet]
        public JsonResult GetAllCompanys()
        {
            string query = @"Select   CompanyName from dbo.Company where Status='A'";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("DBConnection");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult(table);
        }

        [Route("api/Employee/GetAllBranchs")]
        [HttpGet]
        public JsonResult GetAllBranchs()
        {
            string query = @"Select  BranchName from dbo.Branch where Status='A'";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("DBConnection");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult(table);
        }

        [Route("api/Employee/GetAllDesignations")]
        [HttpGet]
        public JsonResult GetAllDesignations()
        {
            string query = @"Select  DesignationName from dbo.Designation where Status='A'";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("DBConnection");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult(table);
        }
    }
}