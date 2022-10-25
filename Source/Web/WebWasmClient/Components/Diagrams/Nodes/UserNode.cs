using Blazor.Diagrams.Core.Models;
using Modules.IdentityModule.Web.DTOs;
using WebWasmClient.Diagrams.Ports;

namespace WebWasmClient.Diagrams.Nodes
{
    public class UserNode : NodeModel
    {
        public UserNode(Blazor.Diagrams.Core.Geometry.Point position = null) : base(position)
        {
            AddPort(new NodePort(this, PortAlignment.Bottom));
        }
        private UserDTO nac;
        public UserDTO SelectedUser
        {
            get
            {
                return nac;
            }
            set
            {
                nac = value;
                Refresh();
            }
        }
    }
}
