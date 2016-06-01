using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using EveryStudioLibrary;

public class SkitSample : MonoBehaviour {

	public enum STEP {
		WAIT,
		END,
		MAX,
	}
	public STEP m_eStep;
	public STEP m_eStepPre;

	[SerializeField]
	private SkitController m_skitController;

	int m_iNetworkSerial;
	public List<SpreadSheetData> m_ssdSample;
	// Use this for initialization
	void Start () {
		m_iNetworkSerial = CommonNetwork.Instance.RecieveSpreadSheet (
			"1n2hFRf0SA-FV_Q5EtZrYwDYUo9EuRgIQkb-PiXZfvqk",
			"ow8ptfs");
		


		m_eStep = STEP.WAIT;
		m_eStepPre = STEP.MAX;
	
	}
	
	// Update is called once per frame
	void Update () {

		bool bInit = false;
		if (m_eStepPre != m_eStep) {
			m_eStepPre  = m_eStep;
			bInit = true;
		}

		switch (m_eStep) {
		case STEP.WAIT:
			if (CommonNetwork.Instance.IsConnected (m_iNetworkSerial)) {
				m_eStep = STEP.END;
			}
			break;

		case STEP.END:

			if (bInit) {
				TNetworkData data = EveryStudioLibrary.CommonNetwork.Instance.GetData (m_iNetworkSerial);
				m_ssdSample = EveryStudioLibrary.CommonNetwork.Instance.ConvertSpreadSheetData (data.m_dictRecievedData);
				CsvSkit skit_data = new CsvSkit ();
				skit_data.Input (m_ssdSample);
				m_skitController.Initialize (skit_data , "1");
			}
			break;
		case STEP.MAX:
		default:
			break;
		}

	
	}
}
