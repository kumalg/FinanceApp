﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finanse.Elements {
    public class ItemCollection : IEnumerable<Operation> {

        private readonly System.Collections.ObjectModel.ObservableCollection<Operation> _itemCollection = new System.Collections.ObjectModel.ObservableCollection<Operation>();

        public IEnumerator<Operation> GetEnumerator() {
            return _itemCollection.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() {
            return GetEnumerator();
        }
        /*
        public void Add(Operation item) {
            _itemCollection.Add(item);
        }
        */
    }
}
