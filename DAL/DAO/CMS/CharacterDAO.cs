using MyOrm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace DAL
{
    public partial class CharacterDAO : CObjectDAO<Character> { }

    public partial class CharacterViewDAO : CObjectViewDAO<Character>
    {
    }
}