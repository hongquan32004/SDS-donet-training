using Microsoft.AspNetCore.Components;
using StudentManagementBlazor.GrpcClient;
using DTOS.Requests;
using DTOS.Responses;
using student = DTOS.Responses.Student;
using AntDesign.Charts;
using Title = AntDesign.Charts.Title;
using Microsoft.JSInterop;
using StudentManagementBlazor.Components.Model;



namespace StudentManagementBlazor.Components.Pages
{
    public partial class Student : ComponentBase
	{
        [Inject] protected StudentGrpcClient StudentGrpcClient { get; set; }
        [Inject] protected IJSRuntime JS { get; set; }
        [Inject] protected NavigationManager Navigation { get; set; }

        private StudentList? students;
		private List<student> Studentdata = new();
		private string TeacherName = "";

		

		public List<ClassStudentStat> ClassStats = new();
		public List<TeacherStudentStat> TeacherStats = new();

		protected override async Task OnInitializedAsync()
		{
			await LoadStudent();
		}

		async Task LoadStudent()
		{
			students = await StudentGrpcClient.GetAllStudents();
			Studentdata = students.Students;

			ClassStats = Studentdata
				.GroupBy(s => s.Classroom.Name)
				.Select(g => new ClassStudentStat
				{
					className = g.Key,
					count = g.Count()
				})
				.ToList();

			TeacherStats = Studentdata
				.GroupBy(s => s.Classroom.Teacher.Name)
				.Select(g => new TeacherStudentStat
				{
					teacherName = g.Key,
					count = g.Count()
				})
				.ToList();
		}

		private BarConfig config1 = new BarConfig
		{
			Title = new Title
			{
				Visible = true,
				Text = "Thống kê sinh viên theo lớp"
			},
			ForceFit = true,
			XField = "count",
			YField = "className",
			Label = new BarViewConfigLabel
			{
				Visible = true
			}
		};

		private BarConfig config2 = new BarConfig
		{
			Title = new Title
			{
				Visible = true,
				Text = "Thống kê sinh viên theo giáo viên"
			},
			ForceFit = true,
			XField = "count",
			YField = "teacherName",
			Label = new BarViewConfigLabel
			{
				Visible = true
			}
		};

		async Task HandleDelete(int studentid)
		{
			try
			{
				var request = new StudentRequest { Id = studentid };
				await StudentGrpcClient.DeleteStudent(request);
			    await LoadStudent();
			    StateHasChanged();

			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		async Task HandleSortByName()
		{
			try
			{
				students = await StudentGrpcClient.SortStudentByName();
				Studentdata = students.Students;
			    StateHasChanged();
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		async Task HandleSortByTeacher()
		{
			try
			{
				if (!string.IsNullOrWhiteSpace(TeacherName))
				{
					var request = new DTOS.Requests.StudentByTeacherRequest { TeacherName = TeacherName };
					students = await StudentGrpcClient.GetStudentByTeacher(request);
					Studentdata = students.Students;      
                    StateHasChanged();
                }
				else
				{
				}
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		async Task HandleExportExcel()
		{
			try
			{
				var result = await StudentGrpcClient.ExportFileExcel();
				var base64 = Convert.ToBase64String(result.fileBytes);
				await JS.InvokeVoidAsync("DownloadFile", result.fileName, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", base64);
				StateHasChanged() ;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		void NavigateUpdate(int id)
		{
			Navigation.NavigateTo($"/update-student/{id}");
		}

		void NavigateDetail(int id)
		{
			Navigation.NavigateTo($"/student-detail/{id}");
		}
	}
}
