using System.Collections.Generic;
using System.Diagnostics;

namespace View.ViewModels
{
    public class TrackQueueViewModel
    {
        public Queue<Model.Track> TrackQueue { get; set; }

        private string _EmptyQueueVisibility;
        public string EmptyQueueVisibility
        {
            get { return _EmptyQueueVisibility; }
            set { _EmptyQueueVisibility = value; }
        }
        public TrackQueueViewModel(Queue<int> combinedQueue)
        {
            EmptyQueueVisibility = "Hidden";
            TrackQueue = new Queue<Model.Track>();
            Trace.WriteLine(combinedQueue.Count);
            if (combinedQueue.Count > 0)
            {
                foreach (var item in combinedQueue)
                {
                    Controller.Track TrackController = new Controller.Track();
                    Model.Track t = TrackController.GetTrack(item);
                    t.Image_path = Controller.ApacheConnection.GetImageFullPath(t.Image_path);
                    if (t != null)
                    {
                        TrackQueue.Enqueue(t);
                    }
                }
            }
            if (TrackQueue.Count > 0)
            {
                EmptyQueueVisibility = "Hidden";
            }
            else
            {
                EmptyQueueVisibility = "Visible";
            }
        }



    }
}
