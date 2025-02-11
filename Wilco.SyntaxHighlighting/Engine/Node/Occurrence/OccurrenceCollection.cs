using System;
using System.Collections;
using System.Collections.ObjectModel;

namespace Wilco.SyntaxHighlighting
{
	/// <summary>
	/// Represents a collection of occurrences.
	/// </summary>
	public class OccurrenceCollection : Collection<Occurrence>
	{
		/// <summary>
		/// Occurs when a new occurrence is added to the <see cref="Wilco.SyntaxHighlighting.OccurrenceCollection"/>.
		/// </summary>
		internal event OccurrenceEventHandler OccurrenceAdded;

		/// <summary>
		/// Occurs when an occurrence is removed from the <see cref="Wilco.SyntaxHighlighting.OccurrenceCollection"/>.
		/// </summary>
		internal event OccurrenceEventHandler OccurrenceRemoved;

		/// <summary>
		/// Initializes a new instance of a <see cref="Wilco.SyntaxHighlighting.OccurrenceCollection"/> class.
		/// </summary>
		public OccurrenceCollection()
		{
			//
		}

        /// <summary>
        /// Inserts an occurrence.
        /// </summary>
        /// <param name="index"></param>
        /// <param name="item"></param>
        protected override void InsertItem(int index, Occurrence item)
        {
            base.InsertItem(index, item);
            this.OnOccurrenceAdded(new OccurrenceEventArgs(item));
        }

        /// <summary>
        /// Removes an occurrence.
        /// </summary>
        /// <param name="index"></param>
        protected override void RemoveItem(int index)
        {
            Occurrence item = this[index];
            base.RemoveItem(index);
            this.OnOccurrenceRemoved(new OccurrenceEventArgs(item));
        }

		/// <summary>
		/// Raises the <see cref="Wilco.SyntaxHighlighting.OccurrenceCollection.OccurrenceAdded"/> event.
		/// </summary>
		/// <param name="e">An <see cref="Wilco.SyntaxHighlighting.OccurrenceEventArgs"/> that contains the event data.</param>
		protected virtual void OnOccurrenceAdded(OccurrenceEventArgs e)
		{
			if (this.OccurrenceAdded != null)
				this.OccurrenceAdded(this, e);
		}

		/// <summary>
		/// Raises the <see cref="Wilco.SyntaxHighlighting.OccurrenceCollection.OccurrenceRemoved"/> event.
		/// </summary>
		/// <param name="e">An <see cref="Wilco.SyntaxHighlighting.OccurrenceEventArgs"/> that contains the event data.</param>
		protected virtual void OnOccurrenceRemoved(OccurrenceEventArgs e)
		{
			if (this.OccurrenceRemoved != null)
				this.OccurrenceRemoved(this, e);
		}
	}
}