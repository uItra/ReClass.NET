﻿using System.Diagnostics.Contracts;
using System.Drawing;
using ReClassNET.UI;

namespace ReClassNET.Nodes
{
	public abstract class BaseNumericNode : BaseNode
	{
		/// <summary>Draws the node.</summary>
		/// <param name="view">The view information.</param>
		/// <param name="x">The x coordinate.</param>
		/// <param name="y">The y coordinate.</param>
		/// <param name="icon">The icon of the node.</param>
		/// <param name="type">The type of the node.</param>
		/// <param name="value">The value of the node.</param>
		/// <returns>The pixel size the node occupies.</returns>
		protected Size DrawNumeric(ViewInfo view, int x, int y, Image icon, string type, string value)
		{
			Contract.Requires(view != null);
			Contract.Requires(icon != null);
			Contract.Requires(type != null);
			Contract.Requires(value != null);

			if (IsHidden)
			{
				return DrawHidden(view, x, y);
			}

			DrawInvalidMemoryIndicator(view, y);

			var origX = x;

			AddSelection(view, x, y, view.Font.Height);

			x += TextPadding;

			x = AddIcon(view, x, y, icon, HotSpot.NoneId, HotSpotType.None);
			x = AddAddressOffset(view, x, y);

			x = AddText(view, x, y, view.Settings.TypeColor, HotSpot.NoneId, type) + view.Font.Width;
			x = AddText(view, x, y, view.Settings.NameColor, HotSpot.NameId, Name) + view.Font.Width;
			x = AddText(view, x, y, view.Settings.NameColor, HotSpot.NoneId, "=") + view.Font.Width;
			x = AddText(view, x, y, view.Settings.ValueColor, 0, value) + view.Font.Width;

			x = AddComment(view, x, y);

			AddTypeDrop(view, y);
			AddDelete(view, y);

			return new Size(x - origX, view.Font.Height);
		}

		public override int CalculateDrawnHeight(ViewInfo view)
		{
			return IsHidden ? HiddenHeight : view.Font.Height;
		}
	}
}
