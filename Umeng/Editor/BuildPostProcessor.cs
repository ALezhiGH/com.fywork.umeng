//
// Copyright (c) 2017 eppz! mobile, Gergely Borbás (SP)
//
// http://www.twitter.com/_eppz
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED,
// INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A
// PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT
// HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF
// CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE
// OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
// 
#if UNITY_2017_1_OR_NEWER

using System.IO;
using UnityEngine;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEditor.iOS.Xcode;


public class BuildPostProcessor
{


	[PostProcessBuildAttribute(1)]
	public static void OnPostProcessBuild(BuildTarget target, string path)
	{
		if (target == BuildTarget.iOS)
		{
			// Read.
			string projectPath = PBXProject.GetPBXProjectPath(path);
			PBXProject project = new PBXProject();
			project.ReadFromString(File.ReadAllText(projectPath));
			//string targetName = PBXProject.GetUnityTargetName();
#if UNITY_2019_3_OR_NEWER
            string targetGUID = project.GetUnityFrameworkTargetGuid();
#else
            string targetName = PBXProject.GetUnityTargetName();
            string targetGUID = project.TargetGuidByName(targetName);
#endif
			

			AddFrameworks(project, targetGUID);
            string[] addBuildProperty = { "$(SRCROOT)/Libraries", "$(SRCROOT)" };
            string[] removeBuildProperty = { "\"$(SRCROOT)/Libraries\"", "\"$(SRCROOT)\"" };

            project.UpdateBuildProperty(targetGUID, "LIBRARY_SEARCH_PATHS", addBuildProperty, removeBuildProperty);

            // Write.
            File.WriteAllText(projectPath, project.WriteToString());
		}
	}

	static void AddFrameworks(PBXProject project, string targetGUID)
	{
		// Frameworks 

		project.AddFrameworkToProject(targetGUID, "libz.dylib", false);
		project.AddFrameworkToProject(targetGUID, "libsqlite3.tbd", false);
        project.AddFrameworkToProject(targetGUID, "CoreTelephony.framework", false);


    }
}

#endif