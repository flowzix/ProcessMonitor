using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessMonitor.Model.Logic
{
    public class TwoDirPresenterSorter
    {
        enum SortDirection{
            ASC, DESC, DEFAULT
        }
        private SortDirection CurrentDirection;
        private ISortsTwoWay SortingSubject;
        public TwoDirPresenterSorter(ISortsTwoWay sortingItem)
        {
            SortingSubject = sortingItem;
            CurrentDirection = SortDirection.DEFAULT;
        }
        public void SortNext()
        {
            switch (CurrentDirection)
            {
                case SortDirection.DEFAULT:
                    SortingSubject.SortAscending();
                    CurrentDirection = SortDirection.ASC;
                    break;
                case SortDirection.ASC:
                    SortingSubject.SortDescending();
                    CurrentDirection = SortDirection.DESC;
                    break;

                case SortDirection.DESC:
                    SortingSubject.SortDefault();
                    CurrentDirection = SortDirection.DEFAULT;
                    break;
            }
        }
        public void SortCurrent()
        {
            switch (CurrentDirection)
            {
                case SortDirection.DEFAULT:
                    SortingSubject.SortDefault();
                    break;
                case SortDirection.ASC:
                    SortingSubject.SortAscending();
                    break;

                case SortDirection.DESC:
                    SortingSubject.SortDescending();
                    break;
            }
        }

    }
}
