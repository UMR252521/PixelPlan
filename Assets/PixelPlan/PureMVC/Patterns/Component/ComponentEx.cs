using UnityEngine;

namespace pixelplan.puremvc {
    public class ComponentEx : MonoBehaviour, INotificationHandler {
        protected IFacade facade {
            get { return Facade.getInstance (); }
        }
        public virtual void Init () { }
        private static int g_MediatorCount = 0;
        protected int m_ID = 0;
        private KMediator _mediator_ = null;
        protected string mediatorName => _mediator_?.mediatorName;

        protected virtual void Awake () {
            this.CreateMediator ();
            m_ID = g_MediatorCount;
        }

        protected virtual void OnDestroy () {
            this.DestoryMediator ();
        }

        private void CreateMediator () {
            this.DestoryMediator ();
            ++g_MediatorCount;
            this._mediator_ = new KMediator (g_MediatorCount.ToString (), this);
        }

        private void DestoryMediator () {
            if (this._mediator_ != null) {
                var mediator = this._mediator_;
                this._mediator_ = null;
                mediator.OnDestroy ();
            }
        }

        public virtual string[] ListNotificationInterests () {
            return new string[0];
        }

        public virtual void HandleNotification (INotification notification) {

        }

        internal class KMediator : Mediator {
            public KMediator (string mediatorName, object _viewComponent = null) : base (mediatorName, _viewComponent) {
                this.facade.RegisterMediator (this);
            }

            public void OnDestroy () {
                this.facade.RemoveMediator (this.mediatorName);
            }

            public override string[] ListNotificationInterests () {
                var _viewComponent = this.viewComponent as ComponentEx;
                return _viewComponent.ListNotificationInterests ();
            }

            public override void HandleNotification (INotification notification) {
                var _viewComponent = this.viewComponent as ComponentEx;
                _viewComponent.HandleNotification (notification);
            }
        }
    }
}