﻿using OpenMB.Game;
using Mogre;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenMB.Script.Command
{
	public class SpawnPlaneScriptCommand : ScriptCommand
	{
		private string[] commandArgs;
		public override string CommandName
		{
			get
			{
				return "spawn_plane";
			}
		}

		public override string[] CommandArgs
		{
			get
			{
				return commandArgs;
			}
		}

		public SpawnPlaneScriptCommand()
		{
			commandArgs = new string[]
			{
				"materialName",
				"rkNormal Vector",
				"constantis",
				"width",
				"height",
				"up Vector",
				"Position Vector",
			};
		}

		public override void Execute(params object[] executeArgs)
		{
			GameWorld world = executeArgs[0] as GameWorld;
			string materialName = getVariableValue(CommandArgs[0]).ToString();
			string rkNormalVectorName = getVariableValue(CommandArgs[1]).ToString();
			string consitantis = getVariableValue(CommandArgs[2]).ToString();
			string width = getVariableValue(CommandArgs[3]).ToString();
			string height = getVariableValue(CommandArgs[4]).ToString();
			string upVectorName = getVariableValue(CommandArgs[5]).ToString();
			string positionVectorName = getVariableValue(CommandArgs[6]).ToString();
			var rkNormalVector = world.GlobalValueTable.GetRecord(rkNormalVectorName);
			var upVector = world.GlobalValueTable.GetRecord(upVectorName);
			var positionVector = world.GlobalValueTable.GetRecord(positionVectorName);

			world.CreatePlane(
				materialName,
				new Vector3(
					float.Parse(rkNormalVector.NextNodes[0].Value),
					float.Parse(rkNormalVector.NextNodes[1].Value),
					float.Parse(rkNormalVector.NextNodes[2].Value)),
				float.Parse(consitantis),
				int.Parse(width),
				int.Parse(height),
				10,
				10,
				0,
				10,
				10,
				new Vector3(
					float.Parse(upVector.NextNodes[0].Value),
					float.Parse(upVector.NextNodes[1].Value),
					float.Parse(upVector.NextNodes[2].Value)),
				new Vector3(
					float.Parse(positionVector.NextNodes[0].Value),
					float.Parse(positionVector.NextNodes[1].Value),
					float.Parse(positionVector.NextNodes[2].Value))
				);
		}
	}
}
