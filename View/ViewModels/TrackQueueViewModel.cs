using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;

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
        public TrackQueueViewModel()
        {
            EmptyQueueVisibility = "Hidden";
            TrackQueue = new Queue<Model.Track>();
            Trace.WriteLine(Controller.TrackQueue.trackQueue.Count);
            if (Controller.TrackQueue.trackQueue.Count > 0)
            {
                foreach(var item in Controller.TrackQueue.trackQueue)
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
