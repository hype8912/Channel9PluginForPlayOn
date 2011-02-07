
namespace Channel9Plugin
{
    using System.ComponentModel;

    /// <summary>
    /// MPAA content ratings.
    /// </summary>
     internal enum ContentRating
     {
         /// <summary>
         /// G rating.
         /// </summary>
         [Description(@"G")]
         G,

         /// <summary>
         /// PG rating.
         /// </summary>
         [Description(@"PG")]
         PG,

         /// <summary>
         /// PG-13 rating.
         /// </summary>
         [Description(@"PG-13")]
         PG13,

         /// <summary>
         /// R rating.
         /// </summary>
         [Description(@"R")]
         R,

         /// <summary>
         /// NC-17 rating.
         /// </summary>
         [Description(@"NC-17")]
         NC17,

         /// <summary>
         /// TV-Y rating.
         /// </summary>
         [Description(@"TV-Y")]
         TVY,

         /// <summary>
         /// TV-Y7 rating.
         /// </summary>
         [Description(@"TV-Y7")]
         TVY7,

         /// <summary>
         /// TV-G rating.
         /// </summary>
         [Description(@"TV-G")]
         TVG,

         /// <summary>
         /// TV-PG rating.
         /// </summary>
         [Description(@"TV-PG")]
         TVPG,

         /// <summary>
         /// TV-14 rating.
         /// </summary>
         [Description(@"TV-14")]
         TV14,

         /// <summary>
         /// TV-MA rating.
         /// </summary>
         [Description(@"TV-MA")]
         TVMA,

         /// <summary>
         /// Not rated.
         /// </summary>
         [Description(@"NR")]
         NR
     }
}