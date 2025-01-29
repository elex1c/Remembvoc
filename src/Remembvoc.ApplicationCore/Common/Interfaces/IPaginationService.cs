using Remembvoc.ApplicationCore.Common.Enums;
using Remembvoc.ApplicationCore.Common.Models;

namespace Remembvoc.ApplicationCore.Common.Interfaces;

public interface IPaginationService
{
    public Pages CurrentPageType { get; set; }
    public Page SwitchPage(Pages pages);
    public Page NextPage();
    public Page PreviousPage();
    public void LoadPageButtons();
    public Page GetCurrentPage();
}