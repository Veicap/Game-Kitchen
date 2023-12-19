using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CuttingCounter;

public class StoveCounter : BaseCounter, IHasProgress
{
    public event EventHandler<IHasProgress.OnBarUIChangedEventArgs> OnBarUIChanged;
    public event EventHandler <OnStateChangedEventArgs> OnStateChanged;
    
    public class OnStateChangedEventArgs: EventArgs
    {
        public State state;
    }
    [SerializeField] private FryingRecipeSO[] fryingRecipeSOArray;
    [SerializeField] private BurningRecipeSO[] burningRecipeSOArray;
    private float fryingTimer;
    private float burningTimer;
    FryingRecipeSO fryingRecipeSO;
    BurningRecipeSO burningRecipeSO;
    public enum State
    {
        Idle,
        Frying,
        Fried,
        Burned,
    }
    private State state;
    private void Start()
    {
        state = State.Idle;
        OnStateChanged?.Invoke(this, new OnStateChangedEventArgs
        {
            state = state
        });
        OnBarUIChanged?.Invoke(this, new IHasProgress.OnBarUIChangedEventArgs
        {
            fillNomarlized = 0f
        });
    }
    private void Update()
    {
        if(HasKitchenObject())
        {
            switch(state)
            {
                case State.Idle:
                    break;
                case State.Frying:

                    fryingTimer += Time.deltaTime;
                    OnBarUIChanged?.Invoke(this, new IHasProgress.OnBarUIChangedEventArgs
                    {
                        fillNomarlized = fryingTimer / fryingRecipeSO.fryingTimerMax
                    });
                    if (fryingTimer > fryingRecipeSO.fryingTimerMax)
                    {
                        GetKitchenObject().DestroySelf();
                        KitchenObject.SpwanKitchenObject(this, fryingRecipeSO.output);
                        Debug.Log(GetKitchenObject().GetKitchenObjectSO());
                        state = State.Fried;
                        burningTimer = 0f;
                        burningRecipeSO = GetInputBurningRecipeSO(GetKitchenObject().GetKitchenObjectSO());
                        Debug.Log(burningRecipeSO);
                        OnStateChanged?.Invoke(this, new OnStateChangedEventArgs
                        {
                            state = state
                        });
                        
                    }
                    break; 
                case State.Fried:
                    burningTimer += Time.deltaTime;
                    OnBarUIChanged?.Invoke(this, new IHasProgress.OnBarUIChangedEventArgs
                    {
                        fillNomarlized = burningTimer / burningRecipeSO.BurningTimerMax
                    });
                    if (burningTimer > burningRecipeSO.BurningTimerMax)
                    {
                        GetKitchenObject().DestroySelf();
                        KitchenObject.SpwanKitchenObject(this, burningRecipeSO.output);
                        state = State.Burned;
                       
                        OnStateChanged?.Invoke(this, new OnStateChangedEventArgs
                        {
                            state = state
                        });
                        OnBarUIChanged?.Invoke(this, new IHasProgress.OnBarUIChangedEventArgs
                        {
                            fillNomarlized = burningTimer / burningRecipeSO.BurningTimerMax
                        });
                    }
                    break;
                case State.Burned:
                    state = State.Idle;
                    break;
            }
            
            
        }
    }
    public override void Interact(Player player)
    {
        if (HasKitchenObject())
        {
            if (!player.HasKitchenObject())
            {
                //player is not carrying anything
                GetKitchenObject().SetKitchenObjectParent(player);
                state = State.Idle;
                OnStateChanged?.Invoke(this, new OnStateChangedEventArgs
                {
                    state = state
                });
                OnBarUIChanged?.Invoke(this, new IHasProgress.OnBarUIChangedEventArgs
                {
                    fillNomarlized = 0f
                });
            }
            else
            {
                //player is carrying anything
                if (player.HasKitchenObject())
                {
                    //player is carrying something
                    if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
                    {

                        if (plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO()))
                        {
                            GetKitchenObject().DestroySelf();
                            state = State.Idle;
                            OnStateChanged?.Invoke(this, new OnStateChangedEventArgs
                            {
                                state = state
                            });
                            OnBarUIChanged?.Invoke(this, new IHasProgress.OnBarUIChangedEventArgs
                            {
                                fillNomarlized = 0f
                            });
                        }
                    }
                }
            }
        }
        else
        {
            if (player.HasKitchenObject())
            {
                if (HasFryingObjectSO(player.GetKitchenObject().GetKitchenObjectSO()))
                {
                    player.GetKitchenObject().SetKitchenObjectParent(this);
                    
                    state = State.Frying;
                    fryingRecipeSO = GetInputfryingRecipeSO(GetKitchenObject().GetKitchenObjectSO());
                    fryingTimer = 0f;
                    OnStateChanged?.Invoke(this, new OnStateChangedEventArgs
                    {
                        state = state
                    });
                    OnBarUIChanged?.Invoke(this, new IHasProgress.OnBarUIChangedEventArgs
                    {
                        fillNomarlized = fryingTimer / fryingRecipeSO.fryingTimerMax
                    });
                }

            }
        }
    }
    private bool HasFryingObjectSO(KitchenObjectSO kitchenObjectSO)
    {
        FryingRecipeSO fryingRecipeSO = GetInputfryingRecipeSO(kitchenObjectSO);
        return fryingRecipeSO != null;
    }
    private KitchenObjectSO GetOutputfromInput(KitchenObjectSO kitchenObjectSO)
    {
        FryingRecipeSO fryingRecipeSO = GetInputfryingRecipeSO(kitchenObjectSO);
        if (fryingRecipeSO != null)
        {
            return fryingRecipeSO.output;
        }
        return null;
    }
    private FryingRecipeSO GetInputfryingRecipeSO(KitchenObjectSO kitchenObjectSO)
    {
        foreach (FryingRecipeSO fryingRecipeSO in fryingRecipeSOArray)
        {
            if (fryingRecipeSO.input == kitchenObjectSO)
            {
                return fryingRecipeSO;
            }

        }
        return null;
    }
    private BurningRecipeSO GetInputBurningRecipeSO(KitchenObjectSO kitchenObjectSO)
    {
        foreach (BurningRecipeSO burningRecipeSO in burningRecipeSOArray)
        {
            if (burningRecipeSO.input == kitchenObjectSO)
            {
                return burningRecipeSO;
            }
        }
        return null;
    }
    public bool IsFried()
    {
        return state == State.Fried; 
    }
}
