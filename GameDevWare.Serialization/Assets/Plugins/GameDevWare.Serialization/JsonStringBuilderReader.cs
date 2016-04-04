﻿/* 
Copyright (c) 2016 Denis Zykov, GameDevWare.com

https://www.assetstore.unity3d.com/#!/content/56706

Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"),
to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, 
and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, 
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, 
WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
*/
using System;
using System.Text;

namespace Serialization.Json
{
	public sealed class JsonStringBuilderReader : JsonReaderBase
	{
		private StringBuilder jsonString;
		private int position;

		public JsonStringBuilderReader(StringBuilder stringBuilder, ISerializationContext context,
			int bufferSize = DEFAULT_BUFFER_SIZE)
			: base(context, bufferSize)
		{
			if (stringBuilder == null)
				throw new ArgumentNullException("str");


			this.jsonString = stringBuilder;
			this.position = 0;
		}

		protected override int FillBuffer(char[] buffer, int index)
		{
			if (buffer == null)
				throw new ArgumentNullException("buffer");
			if (index < 0 || index >= buffer.Length)
				throw new ArgumentOutOfRangeException("index");


			var block = Math.Min(jsonString.Length - position, buffer.Length - index);
			if (block <= 0)
				return index;

			jsonString.CopyTo(position, buffer, index, block);

			position += block;

			return index + block;
		}
	}
}