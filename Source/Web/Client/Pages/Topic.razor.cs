using Blazor.Diagrams.Core;
using Blazor.Diagrams.Core.Options;
using Blazor.Diagrams.Options;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace Web.Client.Pages
{
    public partial class Topic
    {
        [Parameter] public Guid TopicId { get; set; }

        private Diagram diagram;

        protected override async Task OnInitializedAsync()
        {
            //var options = new BlazorDiagramOptions
            //{
            //    DeleteKey = "Delete",
            //    DefaultNodeComponent = null,
            //    AllowMultiSelection = true,
            //    AllowPanning = false,
            //    Zoom = new DiagramZoomOptions
            //    {
            //        Enabled = false
            //    },
            //    Links = new DiagramLinkOptions
            //    {
            //        DefaultColor = "white"
            //    }
            //};
            //diagram = new Diagram(options);


            //StateHasChanged();
            //await Task.Delay(100);
        }

        public void OnDrop(DragEventArgs e)
        {
            //var position = diagram.GetRelativeMousePoint(e.Web.ClientX, e.Web.ClientY);
            //Point point = new Point(position.X, position.Y);
            //NodeModel nodeModel = draggedLearningRessource.LearningRessourceType switch
            //{
            //    "VideoExtern" => new VideoExternNode(point) { LearningRessourceDTO = new LearningRessourceDTO() },
            //    "BlogExtern" => new BlogExternNode(point) { LearningRessourceDTO = new LearningRessourceDTO() },
            //    "Quiz" => new QuizNode(point) { LearningRessourceDTO = new LearningRessourceDTO() { QuizQuestions = new List<QuizQuestionDTO>() } },
            //    _ => null
            //};
            //if (nodeModel != null)
            //{
            //    diagram.Nodes.Add(nodeModel);
            //}
            //draggedLearningRessource = null;
        }
        //private void DragStart(LearningRessourceDisplay learningRessource)
        //{
        //    draggedLearningRessource = learningRessource;
        //}
    }
}
