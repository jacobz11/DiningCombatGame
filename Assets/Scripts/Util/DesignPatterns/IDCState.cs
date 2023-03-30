using System;
using UnityEngine;
namespace DesignPatterns.Abstraction
{
    public interface IDCState
    {
        void OnStateEnter(params object[] list);

        void OnStateUpdate(params object[] list);

        void OnStateExit(params object[] list);

        void OnStateMove(params object[] list);

        void OnStateIK(params object[] list);
        string ToString();
    }   
}