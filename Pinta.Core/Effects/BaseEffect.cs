// 
// BaseEffect.cs
//  
// Author:
//       Jonathan Pobst <monkey@jpobst.com>
// 
// Copyright (c) 2010 Jonathan Pobst
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.

using System;
using Cairo;

namespace Pinta.Core
{
	// TODO consider spliting base effect into BaseEffect and ConfigurationBaseEffectClasses.
	public abstract class BaseEffect
	{
		public abstract string Icon { get; }
		public abstract string Text { get; }
		public virtual bool IsConfigurable { get { return false; } }
		public EffectData EffectData { get; protected set; }
		
		// Return true to perform effect, false to cancel effect
		public virtual bool LaunchConfiguration ()
		{
			if (IsConfigurable)
				throw new NotImplementedException (string.Format ("{0} is marked as configurable, but has not implemented a LaunchConfiguration", this.GetType ()));			
			
			return false;
		}

		public abstract void RenderEffect (ImageSurface src, ImageSurface dest, Gdk.Rectangle[] rois);
				
		// TODO Make this method abstract once all effects have implemented it.
		// I assume that all configurable objects should keep all of their mutable
		// state the EffectData property.
		// (This is needed for the effect to work with LivePreview.)		
		
		// Effects must override this method.
		public virtual BaseEffect Clone ()
		{
			throw new NotImplementedException ();
		}
		
		protected T DoClone<T> ()
			where T : BaseEffect, new ()
		{
			var effect = new T ();
			if (EffectData != null)
				effect.EffectData = EffectData.Clone ();
			return effect;		
		}
	}
	
	public abstract class EffectData : ObservableObject
	{
		public abstract EffectData Clone ();
	}
}
