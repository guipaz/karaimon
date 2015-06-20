﻿//----------------------------------------------
// SQLiter
// Copyright © 2014 OuijaPaw Games LLC
//----------------------------------------------

using UnityEngine;
using System.Data;
using Mono.Data.SqliteClient;
using System.IO;
using System.Collections.Generic;
using SQLiter;

public class MonSQLite : MonoBehaviour
{
	public static MonSQLite Instance = null;
	public bool DebugMode = false;
	private const string SQL_DB_NAME = "Karaimon";
	private static readonly string SQL_DB_LOCATION = "URI=file:" 
		+ Application.dataPath + Path.DirectorySeparatorChar
			+ "Scripts" + Path.DirectorySeparatorChar
			+ "SQL" + Path.DirectorySeparatorChar
			+ "Databases" + Path.DirectorySeparatorChar
			+ SQL_DB_NAME + ".db";
	
	private IDbConnection mConnection = null;
	private IDbCommand mCommand = null;
	private IDataReader mReader = null;
	private string mSQLString;
	private bool mCreateNewTable = false;
	
	void Awake()
	{
		Debug.Log(SQL_DB_LOCATION);
		Instance = this;
		SQLiteInit();
	}

	void OnDestroy()
	{
		SQLiteClose();
	}

	private void SQLiteInit()
	{
		Debug.Log("SQLiter - Opening SQLite Connection");
		mConnection = new SqliteConnection(SQL_DB_LOCATION);
		mCommand = mConnection.CreateCommand();
		mConnection.Open();
		
		// WAL = write ahead logging, very huge speed increase
		mCommand.CommandText = "PRAGMA journal_mode = WAL;";
		mCommand.ExecuteNonQuery();
		
		// journal mode = look it up on google, I don't remember
		mCommand.CommandText = "PRAGMA journal_mode";
		mReader = mCommand.ExecuteReader();
		if (DebugMode && mReader.Read())
			Debug.Log("SQLiter - WAL value is: " + mReader.GetString(0));
		mReader.Close();
		
		// more speed increases
		mCommand.CommandText = "PRAGMA synchronous = OFF";
		mCommand.ExecuteNonQuery();
		
		// and some more
		mCommand.CommandText = "PRAGMA synchronous";
		mReader = mCommand.ExecuteReader();
		if (DebugMode && mReader.Read())
			Debug.Log("SQLiter - synchronous value is: " + mReader.GetInt32(0));
		mReader.Close();
					
		// close connection
		mConnection.Close();
	}
	
	#region Query Values
	public List<Karaimon> GetKaraimon(int id = -1) {
		string select = SQLCreator.MON_SELECT;
		bool single = id > -1;

		if (single) {
			if (select.ToLower().Contains("where")) {
				select += " AND " + id;
			} else {
				select += " WHERE " + id;
			}
		}

		List<Karaimon> monsList = new List<Karaimon> ();
		Debug.Log (select);
		List<Dictionary<string, object>> monHashes = Get (select, single);
		foreach (Dictionary<string, object> entry in monHashes) {
			Karaimon k = new Karaimon(entry);
			if (k != null)
				monsList.Add(k);

			if (single)
				break;
		}

		return monsList;
	}

	public List<Dictionary<string, object>> Get(string select, bool single) {
		mConnection.Open();
		mCommand.CommandText = select;		
		mReader = mCommand.ExecuteReader();
		List<Dictionary<string, object>> list = new List<Dictionary<string, object>>();
		while (mReader.Read())
		{
			Dictionary<string, object> objs = new Dictionary<string, object>();
			int fieldCount = mReader.FieldCount;

			for (int i = 0; i < fieldCount; i++) {
				Debug.Log (mReader.GetName(i) + " " + mReader.GetDataTypeName(i));

				if (mReader.GetFieldType(i).Equals(typeof(string))) {
					objs.Add(mReader.GetName(i), mReader.GetString(i));
				} else if (mReader.GetFieldType(i).Equals(typeof(int))) {
					objs.Add(mReader.GetName(i), mReader.GetInt32(i));
				} else if (mReader.GetFieldType(i).Equals(typeof(float))) {
					objs.Add(mReader.GetName (i), mReader.GetFloat(i));
				} else if (mReader.GetFieldType(i).Equals(typeof(double))) {
					objs.Add(mReader.GetName (i), mReader.GetDouble(i));
				}
			}
			
			list.Add(objs);
			
			string objString = "";
			foreach (KeyValuePair<string, object> obj in objs) {
				objString += obj.Key + ": " + obj.Value + " - ";
			}
			Debug.Log (objString);

			if (single)
				break;
		}
		mReader.Close();
		mConnection.Close();
		
		return list;
	}
	
	public void ExecuteNonQuery(string commandText)
	{
		mConnection.Open();
		mCommand.CommandText = commandText;
		mCommand.ExecuteNonQuery();
		mConnection.Close();
	}
	
	private void SQLiteClose()
	{
		if (mReader != null && !mReader.IsClosed)
			mReader.Close();
		mReader = null;
		
		if (mCommand != null)
			mCommand.Dispose();
		mCommand = null;
		
		if (mConnection != null && mConnection.State != ConnectionState.Closed)
			mConnection.Close();
		mConnection = null;
	}
}

#endregion