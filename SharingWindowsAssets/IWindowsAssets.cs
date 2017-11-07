using System.Collections.Generic;
using System.ServiceModel;

namespace SharingWindowsAssets
{
    [ServiceContract]
    public interface IWindowsAssets
    {
        [OperationContract]
        List<string> ListFoldersRootDirectory();
    }
}
