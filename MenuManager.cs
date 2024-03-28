using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using UnityEngine;

public class MenuManager : MonoBehaviour{

    [SerializeField] private List<MenuScriptParameters> startScripts;

    public void Start(){
        foreach (MenuScriptParameters msp in startScripts){
            string name = msp.getMenuScriptName();
            foreach (MenuScriptMehod methodName in msp.getMenuScriptMethods()){
                ExecuteMenuScriptMethod(name, methodName);
            }
        }
    }

    private void ExecuteMenuScriptMethod(string menuScriptName, MenuScriptMehod method){
        if (menuScriptName == null) throw new InvalidMenuScriptNameException("MenuScript name cannot be null");
        
        System.Type type = System.Type.GetType(menuScriptName);

        if (type == null) throw new MenuScriptNotFoundException("MenuScript not found: " + menuScriptName);

        if (!typeof(MenuScript).IsAssignableFrom(type)) throw new MenuScriptNotFoundException("Introduced name is not a MenuScript component");

        MenuScript ms;
        Component c = gameObject.GetComponent(type);

        if (c == null) ms = (MenuScript)gameObject.AddComponent(type);
        else ms = (MenuScript)c;

        System.Reflection.MethodInfo[] methodsInfo = type.GetMethods().Where(m => m.Name == method.getName()).ToArray();

        if (methodsInfo == null) throw new MenuScriptMethodNotFoundException("Method not found: " + method.getName());

        object[] parameters = new object[method.getParameters().Length];
        System.Reflection.MethodInfo realMethod = null;

        foreach (System.Reflection.MethodInfo mi in methodsInfo){
            if (mi.GetParameters().Length != method.getParameters().Length) continue;
            bool isValid = true;
            System.Reflection.ParameterInfo[] mi_parameters = mi.GetParameters();
            for(int i = 0; i < mi_parameters.Length; i++){

                System.Type mi_paramType = mi_parameters[i].ParameterType;

                try{
                    parameters[i] = Convert.ChangeType(method.getParameters()[i], mi_paramType);
                }catch(Exception){
                    isValid = false;
                    break;
                }

            }

            if (isValid){
                realMethod = mi;
                break;
            }
        }

        if (realMethod == null) throw new MenuScriptMethodNotFoundException("Not found a method with the correct parameters.");

        realMethod.Invoke(ms, parameters);
    }

    [System.Serializable]
    private class MenuScriptParameters{
        [SerializeField] private string menuScriptName;
        [SerializeField] private List<MenuScriptMehod> menuScriptMethods;

        public string getMenuScriptName() => menuScriptName;
        public List<MenuScriptMehod> getMenuScriptMethods() => menuScriptMethods;
    }

    [System.Serializable]
    private class MenuScriptMehod
    {
        [SerializeField] private string methodName;
        [SerializeField] private string[] methodParameters;

        public string getName() => methodName;
        public string[] getParameters() => methodParameters;
    }   
}