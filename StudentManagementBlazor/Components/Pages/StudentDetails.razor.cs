using DTOS.Requests;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.JSInterop;
using StudentManagementBlazor.GrpcClient;

namespace StudentManagementBlazor.Components.Pages
{
    public partial class StudentDetails : ComponentBase
    {
        [Inject] protected StudentGrpcClient StudentGrpcClient { get; set; }
        [Inject] protected IJSRuntime JS { get; set; }
        [Inject] protected NavigationManager Navigation { get; set; }
        [Parameter]
        public int Id { get; set; }
        private DTOS.Responses.Student student;
        protected override async Task OnInitializedAsync()
        {
            var request = new StudentRequest { Id = Id };
            student = await StudentGrpcClient.GetStudentById(request);
        }
    }
}
