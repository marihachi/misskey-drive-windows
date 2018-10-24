using Disboard.Misskey;
using System.Collections.Generic;

namespace MisskeyDriveSync.Models
{
    public class MisskeyDriveTreeNode
    {
        public bool IsRoot => (this.Parent == null);

        public MisskeyDriveTreeNode Parent { get; set; }

        public Disboard.Misskey.Models.Folder Folder { get; set; }

		public List<Disboard.Misskey.Models.File> Files { get; set; } = new List<Disboard.Misskey.Models.File>();

		public List<MisskeyDriveTreeNode> Children { get; set; } = new List<MisskeyDriveTreeNode>();
    }
}
