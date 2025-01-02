using Blazor.Diagrams.Core.Models;
using Blazor.Diagrams.Core.Models.Base;

namespace Shared.Client.Components.Diagrams.Ports
{
    public class NodePort : PortModel
    {
        public NodePort(NodeModel parent, PortAlignment alignment = PortAlignment.Bottom)
            : base(parent, alignment, null, null)
        {

        }

        public override bool CanAttachTo(ILinkable port)
        {
            // Avoid attaching to self port/node
            if (!base.CanAttachTo(port))
                return false;

            return true;
        }
    }
}
