using RecipeDbAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeApp_labb3Db.Presentation.Messages
{
    internal record LoadRecipesMessage(IEnumerable<Recipe> Recipes)
    {

    }
}
