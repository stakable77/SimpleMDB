import { $, apiFetch, renderStatus, clearChildren, getQueryParam, setupPagination, setupPageSizeSelect } from '/scripts/common.js';

(async function initActorsIndex() {
	const page = Math.max(1, Number(getQueryParam('page') || localStorage.getItem('actors_page') || '1'));
	const size = Math.min(100, Math.max(1, Number(getQueryParam('size') || localStorage.getItem('actors_size') || '9')));
	localStorage.setItem('actors_page', page);
	localStorage.setItem('actors_size', size);
	const listEl = $('#actor-list');
	const statusEl = $('#status');
	const tpl = $('#actor-card');
	try {
		const payload = await apiFetch(`/actors?page=${page}&size=${size}`);
		const items = Array.isArray(payload) ? payload : (payload.data || []);
		clearChildren(listEl);
		if (items.length === 0) {
			renderStatus(statusEl, 'warn', 'No actors found for this page.');
		} else {
			renderStatus(statusEl, '', '');
			for (const a of items) {
				const frag = tpl.content.cloneNode(true);
				const root = frag.querySelector('.card');
				root.querySelector('.name').textContent = a.name ?? '—';
				root.querySelector('.age').textContent = `Age ${a.age ?? '—'}`;
				root.querySelector('.btn-view').href = `/actors/view.html?id=${encodeURIComponent(a.id)}`;
				root.querySelector('.btn-edit').href = `/actors/edit.html?id=${encodeURIComponent(a.id)}`;
				root.querySelector('.btn-delete').dataset.id = a.id;
				listEl.appendChild(frag);
			}
		}
		listEl.addEventListener('click', async (ev) => {
			const btn = ev.target.closest('button.btn-delete[data-id]');
			if (!btn) return;
			const id = btn.dataset.id;
			if (!confirm('Delete this actor? This cannot be undone.')) return;
			try {
				await apiFetch(`/actors/${encodeURIComponent(id)}`, { method: 'DELETE' });
				renderStatus(statusEl, 'ok', `Actor ${id} deleted.`);
				setTimeout(() => location.reload(), 2000);
			} catch (err) {
				renderStatus(statusEl, 'err', `Delete failed: ${err.message}`);
			}
		});
		setupPageSizeSelect(size);
		setupPagination(payload, page, size);
	} catch (err) {
		renderStatus(statusEl, 'err', `Failed to fetch actors: ${err.message}`);
	}
})();
