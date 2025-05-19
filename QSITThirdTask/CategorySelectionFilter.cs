using Autodesk.Revit.DB;
using Autodesk.Revit.UI.Selection;

namespace QSITThirdTask
{
    public class CategorySelectionFilter : ISelectionFilter
    {
        private readonly BuiltInCategory _category;

        public CategorySelectionFilter()
        {
            
        }
        public CategorySelectionFilter(BuiltInCategory category)
        {
            _category = category;
        }

        public bool AllowElement(Element elem)
        {
            return elem.Category != null && elem.Category.Id.IntegerValue == (int)_category;
        }

        public bool AllowReference(Reference reference, XYZ position)
        {
            return true;
        }
    }
}
