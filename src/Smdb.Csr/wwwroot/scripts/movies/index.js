import { $, apiFetch, renderStatus, clearChildren, getQueryParam, setupPagination, setupPageSizeSelect } from '/scripts/common.js';

(async function initMoviesIndex() {
	const page = Math.max(1, Number(getQueryParam('page') || localStorage.getItem('page') || '1'));
	const size = Math.min(100, Math.max(1, Number(getQueryParam('size') || localStorage.getItem('size') || '9')));
	localStorage.setItem('page', page);
	localStorage.setItem('size', size);
	const listEl = $('#movie-list');
	const statusEl = $('#status');
	const tpl = $('#movie-card');
	try {
		const payload = await apiFetch(`/movies?page=${page}&size=${size}`);
		const items = Array.isArray(payload) ? payload : (payload.data || []);
		clearChildren(listEl);
		if (items.length === 0) {
			renderStatus(statusEl, 'warn', 'No movies found for this page.');
		} else {
			renderStatus(statusEl, '', '');
			for (const m of items) {
				const frag = tpl.content.cloneNode(true);
				const root = frag.querySelector('.card');
				root.querySelector('.title').textContent = m.title ?? '—';
				root.querySelector('.year').textContent = String(m.year ?? '—');
				root.querySelector('.btn-view').href = `/movies/view.html?id=${encodeURIComponent(m.id)}`;
				root.querySelector('.btn-edit').href = `/movies/edit.html?id=${encodeURIComponent(m.id)}`;
				root.querySelector('.btn-delete').dataset.id = m.id;
				listEl.appendChild(frag);
			}
		}
		listEl.addEventListener('click', async (ev) => {
			const btn = ev.target.closest('button.btn-delete[data-id]');
			if (!btn) return;
			const id = btn.dataset.id;
			if (!confirm('Delete this movie? This cannot be undone.')) return;
			try {
				await apiFetch(`/movies/${encodeURIComponent(id)}`, { method: 'DELETE' });
				renderStatus(statusEl, 'ok', `Movie ${id} deleted.`);
				setTimeout(() => location.reload(), 2000);
			} catch (err) {
				renderStatus(statusEl, 'err', `Delete failed: ${err.message}`);
			}
		});
		setupPageSizeSelect(size);
		setupPagination(payload, page, size);
	} catch (err) {
		renderStatus(statusEl, 'err', `Failed to fetch movies: ${err.message}`);
	}
})();
