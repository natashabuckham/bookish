using System.ComponentModel.DataAnnotations;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;

namespace Bookish.Models;

public class MemberModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
}
