using System;
using System.Collections.Generic;
using System.Text;

namespace MyFm.Core.Contracts
{
    public interface ILiveUpdate
    {
        /// <summary>
        /// Returns specified completion for the query
        /// </summary>
        /// <param name="query">User input</param>
        /// <param name="index">Index of the completion</param>
        /// <returns>Completion text</returns>
        string GetCompletion(State state, string query, int index);

        /// <summary>
        /// Return a tip for the user and\or paint anything in the console
        /// </summary>
        /// <param name="query">User input</param>
        /// <param name="xOffset">Left distance of the origin of the paint area</param>
        /// <param name="yOffset">Top distance of the origin of the paint area</param>
        /// <returns>Tip to render</returns>
        string ShowUpdate(State state, string query, int xOffset, int yOffset);
    }
}
