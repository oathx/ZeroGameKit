﻿// -------------------------------------------------------------------------------
// THIS FILE IS ORIGINALLY GENERATED BY THE DESIGNER.
// YOU ARE ONLY ALLOWED TO MODIFY CODE BETWEEN '///<<< BEGIN' AND '///<<< END'.
// PLEASE MODIFY AND REGENERETE IT IN THE DESIGNER FOR CLASS/MEMBERS/METHODS, ETC.
// -------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

///<<< BEGIN WRITING YOUR CODE FILE_INIT

///<<< END WRITING YOUR CODE

public class NewAgent : behaviac.Agent
///<<< BEGIN WRITING YOUR CODE NewAgent
///<<< END WRITING YOUR CODE
{
	public string name = "";
    public int age = -1;

	public void SayName()
	{
        ///<<< BEGIN WRITING YOUR CODE SayName
        Debug.Log($"[NewAgent] My Name Is: {name}");
        ///<<< END WRITING YOUR CODE
	}

    public void SayAge()
    {
        Debug.Log($"[NewAgent] My Age Is: {age}");
    }

///<<< BEGIN WRITING YOUR CODE CLASS_PART

///<<< END WRITING YOUR CODE

}

///<<< BEGIN WRITING YOUR CODE FILE_UNINIT

///<<< END WRITING YOUR CODE

