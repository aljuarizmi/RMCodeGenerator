using System;
using System.Collections;

namespace Wilco.SyntaxHighlighting
{
	/// <summary>
	/// Represents a collection of occurrences which sorts the occurrences by their start index.
	/// </summary>
	public class SortedOccurrenceCollection : OccurrenceCollection
	{
		/// <summary>
		/// Initializes a new instance of a <see cref="Wilco.SyntaxHighlighting.SortedOccurrenceCollection"/> class.
		/// </summary>
		public SortedOccurrenceCollection()
		{
			//
		}

        /// <summary>
        /// Adds an occurrence.
        /// </summary>
        /// <param name="index"></param>
        /// <param name="item"></param>
        protected override void InsertItem(int index, Occurrence item)
        {
            for (int i = 0; i < this.Count; i++)
            {
                if (item.Start <= this[i].Start)
                {
                    base.InsertItem(i, item);
                    this.OnOccurrenceAdded(new OccurrenceEventArgs(item));
                    return;
                }
            }

            base.InsertItem(index, item);
            this.OnOccurrenceAdded(new OccurrenceEventArgs(item));
        }
	}
}