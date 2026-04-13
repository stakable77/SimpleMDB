import { $, apiFetch, renderStatus, captureUserForm } from '/scripts/common.js';

(async function initUserAdd() {
	const form = $('#user-form');
	const statusEl = $('#status');
	renderStatus(statusEl, 'ok', 'New user. Fill in the details and save.');
	form.addEventListener('submit', async (ev) => {
		ev.preventDefault();
		const payload = captureUserForm(form);
		try {
			const created = await apiFetch('/users', { method: 'POST', body: JSON.stringify(payload) });
			renderStatus(statusEl, 'ok', `Created user #${created.id} "${created.name}".`);
			form.reset();
		} catch (err) {
			renderStatus(statusEl, 'err', `Create failed: ${err.message}`);
		}
	});
})();
