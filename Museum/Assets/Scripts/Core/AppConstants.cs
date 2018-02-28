using System;

public static class AppConstants
{

    #region General Constants

    public static string DefaultEyeTrackingFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\_GazeData";
    public const string DefaultPatientsList = "patientsList.txt";
    #endregion

    #region OSCSystem constants
    public const string CalibrationCubeTag = "CCube";
    public const float CubeScaleRate = 0.75f;
    public const float CubeDistance = 2.0f;
    public const float CubeDepth = 0.1f;
    public const float TimeOutBeforeSizeUp = 1000f;
    public const int GridHeight = 3;
    public const int GridWidth = 3;
    #endregion

    #region LoggerBehavior Constants

    public const string CsvFirstRow = "local_time,milliseconds,timestamp,subject_id,session_id,imageset_id,head_pos_x,head_pos_y,head_pos_z,head_ori_x,head_ori_y,head_ori_z,gaze_hemi_x,gaze_hemi_y,gaze_scene_x,gaze_scene_y,obj_id,obj_pos_x,obj_pos_y,obj_pos_z,gaze_obj_x,gaze_obj_y,gaze_obj_z";

    #endregion
}
