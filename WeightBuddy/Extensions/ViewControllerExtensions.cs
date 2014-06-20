using System;
using MonoTouch.UIKit;
using System.Linq;

namespace iOSLib
{
    public static class ViewControllerExtensions
    {
        #region First Responder Finder

        /// <summary>
        /// Gets the first responder if it is a <c>UITextField</c>.
        /// </summary>
        /// <returns>
        /// The first responder if it is a UITextField, null if it cant find a UITextField 
        /// that is the first responder in the given UIViewController's View hierarchy
        /// </returns>
        /// <param name="viewController">View controller.</param>
        public static UITextField GetUITextFieldFirstResponder(this UIViewController viewController)
        {
            return ParseSubViewsForFirstResponderOfType<UITextField>(viewController.View);
        }

        /// <summary>
        /// Gets the first responder of the view controller that has the given type.
        /// </summary>
        /// <returns>
        /// The first responder of the given type, null if it cant find an instance 
        /// of type T that is a first responder in the View hierarchy.
        /// </returns>
        /// <param name="viewController">View controller.</param>
        /// <typeparam name="T">The 1st type parameter.</typeparam>
        public static T GetFirstResponderOfType<T>(this UIViewController viewController) where T : UIView
        {
            return ParseSubViewsForFirstResponderOfType<T>(viewController.View);
        }

        /// <summary>
        /// Recursively parses through the sub views of the given view to see if any of them are of type <c>T</c>
        /// and identify as the first responder.
        /// </summary>
        /// <returns>The first responder of type T if one is found, <code>default(T)</code> otherwise</returns>
        /// <param name="view">View.</param>
        /// <typeparam name="T">The type  1st type parameter.</typeparam>
        static T ParseSubViewsForFirstResponderOfType<T>(UIView view) where T : UIView
        {
            T firstResponder = default(T);

            // Loop through all of the view's sub views
            foreach (UIView subView in view.Subviews)
            {
                // if it is of type T and it IS the first resonder
                if (subView is T && subView.IsFirstResponder)
                {
                    firstResponder = (T)subView;
                    break;
                }

                // If the sub view has SubViews of its own
                if (subView.Subviews.Any())
                {
                    // recursively parse through those
                    firstResponder = ParseSubViewsForFirstResponderOfType<T>(subView);

                    // if we found a first responder of type T in the recursion somewhere, we can break out
                    if (firstResponder != default(T))
                    {
                        break;
                    }
                }
            }

            return firstResponder;
        }

        #endregion
    }
}

