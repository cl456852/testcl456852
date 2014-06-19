package fileFilter;

import org.gudy.azureus2.plugins.PluginConfig;
import org.gudy.azureus2.plugins.disk.DiskManagerFileInfo;
import org.gudy.azureus2.plugins.download.Download;
import org.gudy.azureus2.plugins.download.DownloadCompletionListener;
import org.gudy.azureus2.plugins.download.DownloadException;
import org.gudy.azureus2.plugins.download.DownloadListener;
import org.gudy.azureus2.plugins.download.DownloadManagerListener;

public class DlListener implements DownloadManagerListener,DownloadListener {
	
	
	PluginConfig pConfig;
	@Override
	public void downloadAdded(Download download) {
		String[] filterStrings =pConfig.getPluginStringParameter("filterStrings", "sample").split(",");
		String[] extStrings=pConfig.getPluginStringParameter("extStrings",".htm,.html,.mht,.ext,.url,.nfo,.txt,.exe").split(",");
		DiskManagerFileInfo[] dmfs=download.getDiskManagerFileInfo();
		for (DiskManagerFileInfo diskManagerFileInfo : dmfs) {
			String pathString=diskManagerFileInfo.getFile().getPath().toLowerCase();
			for (String filterString  : filterStrings) {
				if(pathString.contains(filterString.toLowerCase())){
					diskManagerFileInfo.setSkipped(true);
					break;
				}
			}
			if(!diskManagerFileInfo.isSkipped())
			{
				for (String extString : extStrings) {
					if(pathString.endsWith(extString)){
						diskManagerFileInfo.setSkipped(true);
						break;
					}
				}
			}
		}

	}

	@Override
	public void downloadRemoved(Download download) {
		// TODO Auto-generated method stub

	}

	public PluginConfig getpConfig() {
		return pConfig;
	}

	public void setpConfig(PluginConfig pConfig) {
		this.pConfig = pConfig;
	}

	@Override
	public void stateChanged(Download download, int old_state, int new_state) {
		if(new_state==Download.ST_SEEDING)
			try {
				download.stop();
			} catch (DownloadException e) {
				// TODO Auto-generated catch block
				e.printStackTrace();
			}
		
	}

	@Override
	public void positionChanged(Download download, int oldPosition,
			int newPosition) {
		// TODO Auto-generated method stub
		
	}

	//@Override
//	public void onCompletion(Download d) {
//		try {
//			d.stop();
//		} catch (DownloadException e) {
//			// TODO Auto-generated catch block
//			e.printStackTrace();
//		}
//		
//	}

}
