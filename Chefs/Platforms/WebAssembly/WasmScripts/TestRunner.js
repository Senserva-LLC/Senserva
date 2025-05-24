class SampleRunner {

	static async init() {

		if (!SampleRunner._getCurrentPage) {
			const chefsExports = await Module.getAssemblyExports("Simeserva");

			SampleRunner._getCurrentPage = chefsExports.Simeserva.App.GetCurrentPage;
		}
	}

	static async GetCurrentPage(unused) {
		await SampleRunner.init();
		return SampleRunner._getCurrentPage();
	}
}

globalThis.SampleRunner = SampleRunner;
