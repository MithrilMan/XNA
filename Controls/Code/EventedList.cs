////////////////////////////////////////////////////////////////
//                                                            //
//  Neoforce Controls                                         //
//                                                            //
////////////////////////////////////////////////////////////////
//                                                            //
//         File: EventedList.cs                               //
//                                                            //
//      Version: 0.7                                          //
//                                                            //
//         Date: 11/09/2010                                   //
//                                                            //
//       Author: Tom Shane                                    //
//                                                            //
////////////////////////////////////////////////////////////////
//                                                            //
//  Copyright (c) by Tom Shane                                //
//                                                            //
////////////////////////////////////////////////////////////////

#region //// Using /////////////

////////////////////////////////////////////////////////////////////////////
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections;
using System.Collections.Generic;
////////////////////////////////////////////////////////////////////////////

#endregion

namespace TomShane.Neoforce.Controls {

   public class EventedList<T> : List<T> {
      /// <summary>
      /// Maximum number of items stored, auto delete older message
      /// when a new item has been added within a full list
      /// </summary>
      public int MaxSize { get; set; }

      #region //// Events ////////////

      ////////////////////////////////////////////////////////////////////////////
      public event EventHandler ItemAdded;
      public event EventHandler ItemRemoved;
      ////////////////////////////////////////////////////////////////////////////

      #endregion

      #region //// Constructors //////

      ////////////////////////////////////////////////////////////////////////////
      public EventedList() : base() { }
      public EventedList(int capacity) : base(capacity) { }
      public EventedList(IEnumerable<T> collection) : base(collection) { }
      ////////////////////////////////////////////////////////////////////////////               

      #endregion

      #region //// Methods ///////////

      ////////////////////////////////////////////////////////////////////////////
      public new void Add(T item) {
         int c = this.Count;
         if (MaxSize > 0 && c >= MaxSize) RemoveAt(0);
         base.Add(item);
         if (ItemAdded != null && c != this.Count) ItemAdded.Invoke(this, new EventArgs());
      }
      ////////////////////////////////////////////////////////////////////////////

      ////////////////////////////////////////////////////////////////////////////
      public new void Remove(T obj) {
         int c = this.Count;
         base.Remove(obj);
         if (ItemRemoved != null && c != this.Count) ItemRemoved.Invoke(this, new EventArgs());
      }
      ////////////////////////////////////////////////////////////////////////////

      ////////////////////////////////////////////////////////////////////////////    
      public new void Clear() {
         int c = this.Count;
         base.Clear();
         if (ItemRemoved != null && c != this.Count) ItemRemoved.Invoke(this, new EventArgs());
      }
      ////////////////////////////////////////////////////////////////////////////

      ////////////////////////////////////////////////////////////////////////////
      public new void AddRange(IEnumerable<T> collection) {
         int c = this.Count;
         base.AddRange(collection);
         if (MaxSize > 0 && c >= MaxSize) {
            RemoveRange(0, c - MaxSize);
         }
         if (ItemAdded != null && c != this.Count) ItemAdded.Invoke(this, new EventArgs());
      }
      ////////////////////////////////////////////////////////////////////////////

      ////////////////////////////////////////////////////////////////////////////    
      public new void Insert(int index, T item) {
         int c = this.Count;
         if (MaxSize > 0 && c >= MaxSize) RemoveAt(0);
         base.Insert(index, item);
         if (ItemAdded != null && c != this.Count) ItemAdded.Invoke(this, new EventArgs());
      }
      ////////////////////////////////////////////////////////////////////////////

      ////////////////////////////////////////////////////////////////////////////    
      public new void InsertRange(int index, IEnumerable<T> collection) {
         int c = this.Count;
         base.InsertRange(index, collection);
         if (MaxSize > 0 && c >= MaxSize) {
            RemoveRange(0, c - MaxSize);
         }
         if (ItemAdded != null && c != this.Count) ItemAdded.Invoke(this, new EventArgs());
      }
      ////////////////////////////////////////////////////////////////////////////

      ////////////////////////////////////////////////////////////////////////////
      public new int RemoveAll(Predicate<T> match) {
         int c = this.Count;
         int ret = base.RemoveAll(match);
         if (ItemRemoved != null && c != this.Count) ItemRemoved.Invoke(this, new EventArgs());
         return ret;
      }
      ////////////////////////////////////////////////////////////////////////////

      ////////////////////////////////////////////////////////////////////////////
      public new void RemoveAt(int index) {
         int c = this.Count;
         base.RemoveAt(index);
         if (ItemRemoved != null && c != this.Count) ItemRemoved.Invoke(this, new EventArgs());
      }
      ////////////////////////////////////////////////////////////////////////////

      ////////////////////////////////////////////////////////////////////////////
      public new void RemoveRange(int index, int count) {
         int c = this.Count;
         base.RemoveRange(index, count);
         if (ItemRemoved != null && c != this.Count) ItemRemoved.Invoke(this, new EventArgs());
      }
      ////////////////////////////////////////////////////////////////////////////        

      #endregion

   }

}
