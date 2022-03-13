using Blazor.Diagrams.Core.Models;

namespace WebWasmClient.Diagrams.Ports
{
    public class NodePort : PortModel
    {
        public NodePort(NodeModel parent, PortAlignment alignment = PortAlignment.Bottom)
            : base(parent, alignment, null, null)
        {

        }

        public override bool CanAttachTo(PortModel port)
        {
            // Avoid attaching to self port/node
            if (!base.CanAttachTo(port))
                return false;

            return true;
        }
    }
}
