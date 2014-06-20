using System;
using System.Collections.Generic;
using MonoTouch.Foundation;
using MonoTouch.ObjCRuntime;
using MonoTouch.UIKit;

namespace iOSLib
{
    public static class ViewExtensions
    {
        public static String ViewDisposed = "ViewExtension_ViewDisposed";

        static Dictionary<UIView, Dictionary<UISwipeGestureRecognizerDirection, SwipeClass>> swipeViews = 
            new Dictionary<UIView, Dictionary<UISwipeGestureRecognizerDirection, SwipeClass>>();

        static Dictionary<UIView, Dictionary<uint, TapClass>> tapViews =
            new Dictionary<UIView, Dictionary<uint, TapClass>>();

        static NSObject _touchObserver = NSNotificationCenter.DefaultCenter.AddObserver(ViewDisposed, RemoveView);

        /// <summary>
        /// </summary>            
        public class SwipeClass : NSObject
        {
            public class RecognizerDelegate : UIGestureRecognizerDelegate
            {
                public override bool ShouldReceiveTouch(UIGestureRecognizer recognizer, UITouch touch)
                {
                    return true;
                }
            }

            public readonly UIView View;
            Selector selector = new Selector("Swipe");

            public delegate void D(SwipeClass sender, UISwipeGestureRecognizer recognizer);
            public event D Event = delegate { };

            internal SwipeClass(UIView view)
            {
                View = view;
            }

            internal void InitSwipe(UISwipeGestureRecognizerDirection direction)
            {
                if (!View.RespondsToSelector(selector))
                    {
                        var swipe = new UISwipeGestureRecognizer();
                        swipe.AddTarget(this, selector);
                        swipe.Direction = direction;
                        swipe.Delegate = new RecognizerDelegate();
                        View.AddGestureRecognizer(swipe);
                    }
            }

            [Export("Swipe")]
            void SwipeAction(UISwipeGestureRecognizer recognizer)
            {
                var e = Event;
                e(this, recognizer);
            }
        }

        /// <summary>
        /// Add a swipe event to a View easily, 
        /// and pass the direction and then tack on .Event += ...
        /// </summary>
        /// <param name="view"></param>
        /// <param name="direction"></param>
        /// <returns></returns>
        public static SwipeClass Swipe(this UIView view, UISwipeGestureRecognizerDirection direction)
        {
            Dictionary<UISwipeGestureRecognizerDirection, SwipeClass> inner;
            SwipeClass swipe;

            if (!swipeViews.ContainsKey(view)) 
                {
                    swipeViews.Add(view, inner = new Dictionary<UISwipeGestureRecognizerDirection, SwipeClass>());
                }
            else 
                {
                    inner = swipeViews[view];
                }

            if (!inner.ContainsKey(direction)) 
                {
                    inner.Add(direction, swipe = new SwipeClass(view));
                } 
            else 
                {
                    swipe = inner[direction];
                }

            swipe.InitSwipe(direction);

            return swipe;
        }

        /// <summary>
        /// </summary>            
        public class TapClass : NSObject
        {
            public class RecognizerDelegate : UIGestureRecognizerDelegate
            {
                public override bool ShouldReceiveTouch(UIGestureRecognizer recognizer, UITouch touch)
                {
                    return true;
                }
            }

            public readonly UIView View;
            Selector selector = new Selector("Tap");

            public delegate void D(TapClass sender, UITapGestureRecognizer recognizer);
            public event D Event = delegate { };

            internal TapClass(UIView view)
            {
                View = view;
            }

            internal void InitTap(uint tapCount)
            {
                if (!View.RespondsToSelector(selector))
                    {
                        var tap = new UITapGestureRecognizer();
                        tap.AddTarget(this, selector);
                        tap.NumberOfTapsRequired = tapCount;
                        tap.Delegate = new RecognizerDelegate();
                        View.AddGestureRecognizer(tap);
                    }
            }

            [Export("Tap")]
            void TapAction(UITapGestureRecognizer recognizer)
            {
                var e = Event;
                e(this, recognizer);
            }
        }

        /// <summary>
        /// Add a tap event to a View easily, 
        /// and pass the taps required and then tack on .Event += ...
        /// </summary>
        /// <param name="view"></param>
        /// <param name="tapCount"></param>
        /// <returns></returns>
        public static TapClass Tap(this UIView view, uint tapCount)
        {
            Dictionary<uint, TapClass> inner;
            TapClass tap;

            if (!tapViews.ContainsKey(view)) 
                {
                    tapViews.Add(view, inner = new Dictionary<uint, TapClass>());
                }
            else 
                {
                    inner = tapViews[view];
                }

            if (!inner.ContainsKey(tapCount)) 
                {
                    inner.Add(tapCount, tap = new TapClass(view));
                } 
            else 
                {
                    tap = inner[tapCount];
                }

            tap.InitTap(tapCount);

            return tap;
        }

        /// <summary>
        /// Removes the view when it is disposed.
        /// </summary>
        /// <param name='ntf'>
        /// The notification.
        /// </param>
        static void RemoveView(NSNotification ntf)
        {
            var view = ntf.Object as UIView;

            if (view == null)
                {
                    // TODO: throw an exception?
                    return;
                }

            if (swipeViews.ContainsKey(view))
                {
                    swipeViews.Remove(view);
                }

            if (tapViews.ContainsKey(view))
                {
                    tapViews.Remove(view);
                }
        }
    }
}